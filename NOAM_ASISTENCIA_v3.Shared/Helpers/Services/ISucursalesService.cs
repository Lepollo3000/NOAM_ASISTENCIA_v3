using NOAM_ASISTENCIA_v3.Shared.Features;
using NOAM_ASISTENCIA_v3.Shared.Features.Sucursales;
using NOAM_ASISTENCIA_v3.Shared.Helpers.Paging;

namespace NOAM_ASISTENCIA_v3.Shared.Helpers.Services;

public interface ISucursalesService
{
    /// <summary>
    /// Obtiene las sucursales tomando en cuenta los filtros (<see cref="SucursalSearch"/>), el ordenamiento (<see cref="SucursalOrder"/>) y la paginación (<see cref="PageParameters"/>).
    /// </summary>
    /// <returns>Una lista paginada de sucursales en un formato propio para el front-end.</returns>>
    Task<PagedList<SucursalTableModel>> GetSucursalesAsync(SearchParameters<SucursalOrder, SucursalSearch> searchModel);
}
