using NOAM_ASISTENCIA_v3.Shared.Helpers.Services;

namespace NOAM_ASISTENCIA_v3.Server.Helpers.Services;

public class ApplicationServices(IUserService userService/*, IAccountService accountService*/, ISucursalesService sucursalesService) : IApplicationServices
{
    public IUserService Users => userService;
    //public IAccountService Accounts => accountService;
    public ISucursalesService Sucursales => sucursalesService;
}
