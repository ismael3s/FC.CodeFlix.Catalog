namespace FC.CodeFlix.Catalog.Application.Common;

public abstract class PaginatedListOutput<TOutput>
{
    public int Page { get; set; }
    public int PerPage { get; set; }
    public int Total { get; set; }
    public IReadOnlyList<TOutput> Items { get; }
    protected PaginatedListOutput(int page, int perPage, int total, IReadOnlyList<TOutput> items)
    {
        Page = page;
        PerPage = perPage;
        Total = total;
        Items = items;
    }
}
