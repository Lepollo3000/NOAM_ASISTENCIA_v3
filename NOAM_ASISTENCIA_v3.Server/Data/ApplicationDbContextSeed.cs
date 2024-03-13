using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NOAM_ASISTENCIA_v3.Server.Domain;

namespace NOAM_ASISTENCIA_v3.Server.Data;

public class ApplicationDbContextSeed : IHostedService
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _dbcontext;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ILogger<ApplicationDbContextSeed> _logger;

    public ApplicationDbContextSeed(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        var scope = serviceProvider.CreateAsyncScope();

        _dbcontext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
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
            //await SeedDefaultData();
            await SeedDefaultUsersAndRoles();
        }
    }

    private async Task<bool> TryToMigrate()
    {
        try
        {
            await _dbcontext.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error al migrar la base de datos.");

            return false;
        }

        return true;
    }
    #endregion

    #region Seed default data
    #endregion

    #region Seed default user and roles
    private async Task SeedDefaultUsersAndRoles()
    {
        try
        {
            string adminRole = "Administrador";
            TempUser adminUser = new(
                name: "administrador",
                email: "",
                password: "Pa55w.rd",
                nombre: "Usuario",
                apellido: "Administrador",
                roles: [adminRole]);

            string gerenteRole = "Gerente";
            TempUser gerenteUser = new(
                name: "gerente",
                email: "",
                password: "Pa55w.rd",
                nombre: "Usuario",
                apellido: "Gerente",
                roles: [gerenteRole]);

            string intendenteRole = "Intendente";
            TempUser intendenteUser = new(
                name: "intendente",
                email: "",
                password: "Pa55w.rd",
                nombre: "Usuario",
                apellido: "Intendente",
                roles: [intendenteRole]);

            IEnumerable<string> roles = [adminRole, gerenteRole, intendenteRole];
            IEnumerable<TempUser> users = [adminUser, gerenteUser, intendenteUser];

            await CreateRolesIfDontExist(roles);
            await CreateUsersIfDontExist(users);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error al crear roles y usuarios.");
        }
    }

    private async Task CreateRolesIfDontExist(IEnumerable<string> roles)
    {
        foreach ((string role, int i) in roles.Select((role, i) => (role, i)))
        {
            ApplicationRole? oRole = await _roleManager.FindByNameAsync(role);

            if (oRole == null)
            {
                oRole = new ApplicationRole()
                {
                    Name = role
                };

                await _roleManager.CreateAsync(oRole);
            }
        }
    }

    private async Task CreateUsersIfDontExist(IEnumerable<TempUser> users)
    {
        foreach ((TempUser user, int i) in users.Select((user, i) => (user, i)))
        {
            ApplicationUser? oUser = await _userManager.FindByNameAsync(user.Name);

            if (oUser == null)
            {
                oUser = new ApplicationUser()
                {
                    UserName = user.Name,
                    Email = user.Email,
                    Nombres = user.Nombre,
                    Apellidos = user.Apellido,
                    TurnoId = null
                };

                await _userManager.CreateAsync(oUser, user.Password);

                string token = await _userManager.GenerateEmailConfirmationTokenAsync(oUser);
                await _userManager.ConfirmEmailAsync(oUser, token);

                if (user.Roles.Any())
                {
                    await _userManager.AddToRolesAsync(oUser, user.Roles);
                }
            }
        }
    }
    #endregion

    #region Clases temporales
    private class TempUser(string name, string email, string password, string nombre, string apellido, IEnumerable<string> roles)
    {
        public string Name { get; set; } = name;
        public string Email { get; set; } = email;
        public string Password { get; set; } = password;
        public string Nombre { get; set; } = nombre;
        public string Apellido { get; set; } = apellido;
        public IEnumerable<string> Roles { get; set; } = roles;
    }

    private class TempTurno(int id, string descripcion)
    {
        public int Id { get; } = id;
        public string Descripcion { get; } = descripcion;
    }

    private class TempServicio(int id, string codigoId, string descripcion)
    {
        public int Id { get; } = id;
        public string CodigoId { get; } = codigoId;
        public string Descripcion { get; } = descripcion;
    }
    #endregion

}
