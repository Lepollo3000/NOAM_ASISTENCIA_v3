using NOAM_ASISTENCIA_v3.Shared.Features;
using NOAM_ASISTENCIA_v3.Shared.Features.Sucursales;

namespace NOAM_ASISTENCIA_v3.Shared.Helpers.Services;

public interface ISucursalesService
{
    /// <summary>
    /// Obtiene las sucursales tomando en cuenta
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<SucursalTableModel>> GetSucursales(SearchParameters<SucursalOrder, SucursalSearch> searchModel);
}
