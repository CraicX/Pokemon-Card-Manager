namespace PokeCardManager.Data;
public class ResultSet
{
    public int Count { get; set; } = 0;

    public int TotalCount { get; set; } = 0;

    public int Page { get; set; } = 0;

    public int PageSize { get; set; } = 0;

    public bool FromCache { get; set; } = false;
}
