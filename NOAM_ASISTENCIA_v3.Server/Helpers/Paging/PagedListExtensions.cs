using Microsoft.EntityFrameworkCore;
using NOAM_ASISTENCIA_v3.Shared.Features;
using NOAM_ASISTENCIA_v3.Shared.Helpers.Paging;

namespace NOAM_ASISTENCIA_v3.Server.Helpers.Paging;

public static class PagedListExtensions
{
    public static PagedList<T> ToPagedList<T>(this IEnumerable<T> items, int totalCount, PageParameters pageParameters)
    {
        return new(items, totalCount, pageParameters.PageSize, pageParameters.PageNumber);
    }

    public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> query, PageParameters pageParameters)
    {
        int totalCount = await query.CountAsync();

        var items = await query
            .Skip(pageParameters.PageNumber
                * pageParameters.PageSize)
            .Take(pageParameters.PageSize)
            .ToListAsync();

        return new(items, totalCount, pageParameters.PageSize, pageParameters.PageNumber);
    }
}
