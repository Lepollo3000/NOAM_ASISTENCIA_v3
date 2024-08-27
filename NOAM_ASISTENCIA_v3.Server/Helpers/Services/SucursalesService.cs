using Mapster;
using Microsoft.EntityFrameworkCore;
using NOAM_ASISTENCIA_v3.Server.Data;
using NOAM_ASISTENCIA_v3.Server.Domain;
using NOAM_ASISTENCIA_v3.Server.Helpers.Paging;
using NOAM_ASISTENCIA_v3.Shared.Features;
using NOAM_ASISTENCIA_v3.Shared.Features.Sucursales;
using NOAM_ASISTENCIA_v3.Shared.Helpers.Paging;
using NOAM_ASISTENCIA_v3.Shared.Helpers.Services;

namespace NOAM_ASISTENCIA_v3.Server.Helpers.Services;

public class SucursalesService(ApplicationDbContext context) : ISucursalesService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<PagedList<SucursalTableModel>> GetSucursalesAsync(SearchParameters<SucursalOrder, SucursalSearch> searchModel)
    {
        return await _context.Sucursales
            .WhereSearchParameters(searchModel.SearchTerm)
            .OrderByParameters(searchModel.OrderTerm)
            .ProjectToType<SucursalTableModel>()
            .ToPagedListAsync(searchModel.PageParameters);
    }
}

public static class SucursalesExtensions
{
    public static IQueryable<Sucursal> WhereSearchParameters(this IQueryable<Sucursal> query, SucursalSearch searchModel)
    {
        if (searchModel.SucursalId != null)
        {
            query = query
                .Where(model => model.Id.Value
                    == searchModel.SucursalId);
        }
        else
        {
            query = query
                .Where(model => searchModel.Descripcion == null
                    || EF.Functions.Like(model.Descripcion,
                        "%" + searchModel.Descripcion + "%"))
                .Where(model => searchModel.EstaEliminado == null
                    || model.EstaEliminado == searchModel.EstaEliminado);
        }

        return query;
    }

    public static IQueryable<Sucursal> OrderByParameters(this IQueryable<Sucursal> query, SucursalOrder orderModel)
    {
        if (orderModel.BDescripcion)
        {
            query = query
                .OrderBy(model => model.Descripcion);
        }

        return query;
    }
}
