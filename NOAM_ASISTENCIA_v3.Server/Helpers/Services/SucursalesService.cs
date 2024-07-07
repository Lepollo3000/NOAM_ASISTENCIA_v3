using NOAM_ASISTENCIA_v3.Shared.Features;
using NOAM_ASISTENCIA_v3.Shared.Features.Sucursales;
using NOAM_ASISTENCIA_v3.Shared.Helpers.Services;

namespace NOAM_ASISTENCIA_v3.Server.Helpers.Services;

public class SucursalesService : ISucursalesService
{
    public Task<IEnumerable<SucursalTableModel>> GetSucursales(SearchParameters<SucursalOrder, SucursalSearch> searchModel)
    {
        throw new NotImplementedException();
    }
}
