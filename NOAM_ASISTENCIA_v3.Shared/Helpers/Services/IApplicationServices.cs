namespace NOAM_ASISTENCIA_v3.Shared.Helpers.Services;

public interface IApplicationServices
{
    IUserService Users { get; }
    ISucursalesService Sucursales { get; }
}
