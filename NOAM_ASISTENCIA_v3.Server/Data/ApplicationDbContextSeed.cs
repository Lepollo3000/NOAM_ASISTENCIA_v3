using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NOAM_ASISTENCIA_v3.Server.Domain;
using NOAM_ASISTENCIA_v3.Server.Helpers.Identity;
using NOAM_ASISTENCIA_v3.Shared.Helpers.StronglyTypedIds;

namespace NOAM_ASISTENCIA_v3.Server.Data;

public class ApplicationDbContextSeed : IHostedService
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<Usuario> _userManager;
    private readonly RoleManager<Rol> _roleManager;
    private readonly ILogger<ApplicationDbContextSeed> _logger;

    public ApplicationDbContextSeed(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        var scope = serviceProvider.CreateAsyncScope();

        _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        _userManager = scope.ServiceProvider.GetRequiredService<UserManager<Usuario>>();
        _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Rol>>();
        _logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContextSeed>>();

        _configuration = configuration;
    }

    public async Task StartAsync(CancellationToken cancellationToken) => await InitializeDatabase();

    public async Task StopAsync(CancellationToken cancellationToken) => await Task.CompletedTask;

    #region Initialize database
    private async Task InitializeDatabase()
    {
        if (await TryToMigrate())
        {
            IEnumerable<TempTurno> turnos = await SeedDefaultData();
            await SeedDefaultUsersAndRoles(turnos: turnos);
        }
    }

    private async Task<bool> TryToMigrate()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error al migrar la base de datos. '{ex}'.", ex);

            return false;
        }

        return true;
    }
    #endregion

    #region Seed super user
    private async Task SeedSuperUser()
    {
        await SeedDefaultUsersAndRoles(turnos: [], insertSuperUser: true);
    }

    private async Task<Usuario?> GetSuperUser()
    {
        return await _userManager.FindByNameAsync("superusuario");
    }
    #endregion

    #region Seed default data
    private async Task<IEnumerable<TempTurno>> SeedDefaultData()
    {
        try
        {
            await SeedSuperUser();

            Usuario? usuario = await GetSuperUser();

            if (usuario != null)
            {
                await InsertDefaultSucursales(usuario);
                TempTurno turno = await InsertDefaultTurnos(usuario);

                return [turno];
            }

            throw new InvalidOperationException("No se encontró un usuario para crear los datos predeterminados.");
        }
        catch (Exception ex)
        {
            _logger.LogError("Error al crear datos predeterminados. '{ex}'.", ex.Message);

            throw;
        }
    }

    private async Task InsertDefaultSucursales(Usuario usuario)
    {
        List<TempSucursal> tempSucursales =
        [
            new(Id: new(1), CodigoId: "3974", Descripcion: "BOWLING MONTERREY"),
            new(Id: new(2), CodigoId: "4010", Descripcion: "SMART FIT PLAZA TITAN MTY"),
            new(Id: new(3), CodigoId: "4011", Descripcion: "SMART FIT MULTIPLAZA MTY"),
            new(Id: new(4), CodigoId: "4012", Descripcion: "SMART FIT PLAZA FIESTA MTY"),
            new(Id: new(5), CodigoId: "4017", Descripcion: "SMART FIT STA CATARINA MTY")
        ];

        foreach (TempSucursal tempSucursal in tempSucursales)
        {
            bool existe = await _context.Sucursales
                .Where(model => model.Id == tempSucursal.Id)
                .AnyAsync();

            if (!existe)
            {
                Sucursal sucursal = new()
                {
                    Id = tempSucursal.Id,
                    CodigoId = tempSucursal.CodigoId,
                    Descripcion = tempSucursal.Descripcion,

                    FechaUtcAlta = DateTime.UtcNow,
                    UsuarioAltaId = usuario.Id
                };

                _context.Add(sucursal);
            }
        }

        await _context.SaveChangesWithIdentityInsertAsync<Sucursal>();
    }

    private async Task<TempTurno> InsertDefaultTurnos(Usuario usuario)
    {
        IEnumerable<TempTurno> tempTurnos =
        [
            new(Id: new(1),
                HoraInicio: new TimeOnly(hour: 08, minute: 30),
                HoraFin: new TimeOnly(hour: 14, minute: 00),
                Dias:
                [
                    new(DayOfWeek.Monday),
                    new(DayOfWeek.Tuesday),
                    new(DayOfWeek.Wednesday),
                    new(DayOfWeek.Thursday),
                    new(DayOfWeek.Friday)
                ]),
            new(Id: new(2),
                HoraInicio: new TimeOnly(hour: 14, minute: 00),
                HoraFin: new TimeOnly(hour: 20, minute: 30),
                Dias:
                [
                    new(DayOfWeek.Monday),
                    new(DayOfWeek.Tuesday),

                    new(DayOfWeek.Wednesday),
                    new(DayOfWeek.Thursday),
                    new(DayOfWeek.Friday)
                ]),
        ];

        foreach (TempTurno tempTurno in tempTurnos)
        {
            bool existe = await _context.Turnos
                .Where(model => model.Id == tempTurno.Id)
                .AnyAsync();

            if (!existe)
            {
                Turno turno = new()
                {
                    HoraInicio = tempTurno.HoraInicio,
                    HoraFin = tempTurno.HoraFin,

                    FechaUtcAlta = DateTime.UtcNow,
                    UsuarioAltaId = usuario.Id
                };

                _context.Add(turno);

                foreach (TempTurnoDia tempTurnoDia in tempTurno.Dias)
                {
                    TurnoDia turnoDia = new()
                    {
                        Turno = turno,
                        Dia = tempTurnoDia.Dia,

                        FechaUtcAlta = DateTime.UtcNow,
                        UsuarioAltaId = usuario.Id
                    };

                    _context.Add(turnoDia);
                }
            }
        }

        await _context.SaveChangesAsync();

        return tempTurnos.First();
    }
    #endregion

    #region Seed default user and roles
    private async Task SeedDefaultUsersAndRoles(IEnumerable<TempTurno> turnos, bool insertSuperUser = false)
    {
        try
        {
            string adminRole = "Administrador";
            string gerenteRole = "Gerente";
            string intendenteRole = "Intendente";

            TempUser adminUser = new(
                Name: "administrador",
                Email: string.Empty,
                Password: "Pa55w.rd",
                Nombre: "Usuario",
                Apellido: "Administrador",
                Roles: [adminRole],
                Turnos: []);

            TempUser gerenteUser = new(
                Name: "gerente",
                Email: string.Empty,
                Password: "Pa55w.rd",
                Nombre: "Usuario",
                Apellido: "Gerente",
                Roles: [gerenteRole],
                Turnos: []);

            TempUser intendenteUser = new(
                Name: "intendente",
                Email: string.Empty,
                Password: "Pa55w.rd",
                Nombre: "Usuario",
                Apellido: "Intendente",
                Roles: [intendenteRole],
                Turnos: turnos);

            TempUser superUser = new(
                Name: "superusuario",
                Email: string.Empty,
                Password: "Pa55w.rd",
                Nombre: "Usario",
                Apellido: "Administrador",
                Roles: [adminRole, gerenteRole, intendenteRole],
                Turnos: []);

            IEnumerable<string> roles = [adminRole, gerenteRole, intendenteRole];
            IEnumerable<TempUser> tempUsers = insertSuperUser switch
            {
                true => [superUser],
                false => [adminUser, gerenteUser, intendenteUser]
            };
            Usuario? superUsuario = insertSuperUser switch
            {
                true => null,
                false => await GetSuperUser()
            };

            await CreateRolesIfDontExist(roles);
            await CreateUsersIfDontExist(tempUsers: tempUsers, superUsuario: superUsuario);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error al crear usuario y roles predeterminados. '{ex}'.", ex.Message);
        }
    }

    private async Task CreateRolesIfDontExist(IEnumerable<string> roles)
    {
        foreach (string role in roles)
        {
            Rol? model = await _roleManager.FindByNameAsync(role);

            if (model == null)
            {
                model = new Rol()
                {
                    Name = role
                };

                await _roleManager.CreateAsync(model);
            }
        }
    }

    private async Task CreateUsersIfDontExist(IEnumerable<TempUser> tempUsers, Usuario? superUsuario)
    {
        foreach (TempUser tempUser in tempUsers)
        {
            Usuario? user = await _userManager.FindByNameAsync(tempUser.Name);

            if (user == null)
            {
                user = new Usuario()
                {
                    UserName = tempUser.Name,
                    Email = tempUser.Email,
                    Nombres = tempUser.Nombre,
                    Apellidos = tempUser.Apellido
                };

                await _userManager.CreateAsync(user, tempUser.Password);
                string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                await _userManager.ConfirmEmailAsync(user, token);

                if (tempUser.Roles.Any())
                {
                    await _userManager.AddToRolesAsync(user, tempUser.Roles);
                }

                if (tempUser.Turnos.Any() && superUsuario != null)
                {
                    foreach (TempTurno turno in tempUser.Turnos)
                    {
                        UsuarioTurno usuarioTurno = new()
                        {
                            TurnoId = turno.Id,
                            UsuarioId = user.Id,

                            FechaUtcAlta = DateTime.UtcNow,
                            UsuarioAltaId = superUsuario.Id
                        };

                        _context.Add(usuarioTurno);
                    }

                    await _context.SaveChangesAsync();
                }
            }
        }
    }
    #endregion

    #region Clases temporales
    private record TempUser(string Name, string Email, string Password, string Nombre, string Apellido, IEnumerable<string> Roles, IEnumerable<TempTurno> Turnos);

    private record TempSucursal(SucursalId Id, string CodigoId, string Descripcion);

    private record TempTurnoDia(DayOfWeek Dia);
    private record TempTurno(TurnoId Id, TimeOnly HoraInicio, TimeOnly HoraFin, IEnumerable<TempTurnoDia> Dias);
    #endregion
}
