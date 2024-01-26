
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NOAM_ASISTENCIA_v3.Server.Domain;
using NOAM_ASISTENCIA_v3.Server.Models;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace NOAM_ASISTENCIA_v3.Server.Data;

public class ApplicationDbContextSeed : IHostedService
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _dbcontext;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ILogger<ApplicationDbContextSeed> _logger;
    private readonly IOpenIddictApplicationManager _openIddictManager;

    public ApplicationDbContextSeed(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        var scope = serviceProvider.CreateAsyncScope();

        _dbcontext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        _logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContextSeed>>();
        //_openIddictManager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

        _configuration = configuration;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await InitializeDatabase();
        //await CreateAppDescriptors();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

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
        catch (Exception)
        {
            _logger.Log(LogLevel.Error, "Error al migrar la base de datos.");

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
            var adminRole = "Administrador";
            var adminUser = new TempUser(name: "administrador", email: "", password: "Pa55w.rd", nombre: "Usuario", apellido: "Administrador", roles: new List<string>() { adminRole });

            var gerenteRole = "Gerente";
            var gerenteUser = new TempUser(name: "gerente", email: "", password: "Pa55w.rd", nombre: "Usuario", apellido: "Gerente", roles: new List<string>() { gerenteRole });

            var intendenteRole = "Intendente";
            var intendenteUser = new TempUser(name: "intendente", email: "", password: "Pa55w.rd", nombre: "Usuario", apellido: "Itendente", roles: new List<string>() { intendenteRole });

            var roles = new List<string>() { adminRole, gerenteRole, intendenteRole };
            var users = new List<TempUser>() { adminUser, gerenteUser, intendenteUser };

            await CreateRolesIfDontExist(roles);
            await CreateUsersIfDontExist(users);
        }
        catch (Exception)
        {
            _logger.Log(LogLevel.Error, "Error al crear roles y usuarios.");
        }
    }

    private async Task CreateRolesIfDontExist(IEnumerable<string> roles)
    {
        foreach (string role in roles)
        {
            ApplicationRole? oRole = await _roleManager.FindByNameAsync(role);

            if (oRole == null)
            {
                oRole = new ApplicationRole()
                {
                    //Id = Guid.NewGuid(),
                    Name = role
                };

                await _roleManager.CreateAsync(oRole);
            }
        }
    }

    private async Task CreateUsersIfDontExist(IEnumerable<TempUser> users)
    {
        foreach (TempUser user in users)
        {
            ApplicationUser? oUser = await _userManager.FindByNameAsync(user.Name);

            if (oUser == null)
            {
                oUser = new ApplicationUser()
                {
                    //Id = Guid.NewGuid(),
                    UserName = user.Name,
                    Email = user.Email,
                    Nombre = user.Nombre,
                    Apellido = user.Apellido,
                    IdTurno = 1
                };

                // CREATE USER
                await _userManager.CreateAsync(oUser, user.Password);
                // CONFIRM EMAIL
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(oUser);
                await _userManager.ConfirmEmailAsync(oUser, token);

                if (user.Roles.Any())
                {
                    await _userManager.AddToRolesAsync(oUser, user.Roles);
                }
            }
        }
    }
    #endregion

    #region APP_DESCRIPTOR
    private async Task CreateAppDescriptors()
    {
        try
        {
            TempAppDescriptor[] appDescriptorSection = _configuration
                .GetSection("OpenIdAppDescriptors")
                .Get<TempAppDescriptor[]>()
                    ?? throw new InvalidOperationException("Se debe configurar al menos un descriptor para OpendId Connect.");

            foreach (TempAppDescriptor appDescriptor in appDescriptorSection)
            {
                // SI NO SE ENCUENTRA EL DESCRIPTOR CON SU ID, LO CREA
                if (await _openIddictManager.FindByClientIdAsync(appDescriptor.ClientId) is null)
                {
                    await _openIddictManager.CreateAsync(new OpenIddictApplicationDescriptor
                    {
                        ClientId = appDescriptor.ClientId,
                        ConsentType = ConsentTypes.Explicit,
                        DisplayName = "Blazor Client Application",
                        ClientType = ClientTypes.Public,
                        PostLogoutRedirectUris =
                        {
                            new Uri($"{appDescriptor.BaseUrl}authentication/logout-callback")
                        },
                        RedirectUris =
                        {
                            new Uri($"{appDescriptor.BaseUrl}authentication/login-callback")
                        },
                        Permissions =
                        {
                            Permissions.Endpoints.Authorization,
                            Permissions.Endpoints.Logout,
                            Permissions.Endpoints.Token,
                            Permissions.GrantTypes.AuthorizationCode,
                            Permissions.GrantTypes.RefreshToken,
                            Permissions.ResponseTypes.Code,
                            Permissions.Scopes.Email,
                            Permissions.Scopes.Profile,
                            Permissions.Scopes.Roles
                        },
                        Requirements =
                        {
                            Requirements.Features.ProofKeyForCodeExchange
                        }
                    });
                }
            }
        }
        catch (Exception)
        {
            _logger.Log(LogLevel.Error, "Error al intentar crear App Descriptors");

            throw;
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

    private class TempAppDescriptor
    {
        public string ClientId { get; set; } = null!;
        public string BaseUrl { get; set; } = null!;
    }
    #endregion

}
