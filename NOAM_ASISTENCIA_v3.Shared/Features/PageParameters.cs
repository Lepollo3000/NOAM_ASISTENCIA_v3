namespace NOAM_ASISTENCIA_v3.Shared.Features;

public class PageParameters
{
    public static int MaxPageSize = 50;
    public static int DefaultPageSize = 10;
    public static int DefaultPageNumber = 1;

    private int _pageSize = DefaultPageSize;
    private int _pageNumber = DefaultPageNumber;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = (value < DefaultPageNumber) ? DefaultPageNumber : value;
    }
}
