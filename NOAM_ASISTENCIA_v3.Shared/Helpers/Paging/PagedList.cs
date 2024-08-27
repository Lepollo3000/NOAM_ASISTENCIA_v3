namespace NOAM_ASISTENCIA_v3.Shared.Helpers.Paging;

public class PagedList<T>(IEnumerable<T> items, int totalCount, int pageSize, int pageNumber) : List<T>
{
    public IEnumerable<T> Items { get; private set; } = items;

    public int CurrentPage { get; private set; } = pageNumber;
    public int TotalPages { get; private set; } = (int)Math.Ceiling(totalCount / (double)pageSize);
    public int TotalCount { get; private set; } = totalCount;
    public int PageSize { get; private set; } = pageSize;

    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;
}
