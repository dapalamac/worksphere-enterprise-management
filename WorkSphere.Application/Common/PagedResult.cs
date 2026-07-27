namespace WorkSphere.Application.Common;

public class PagedResult<T>
{
    public List<T> Items { get; set; } = [];

    public PaginationMetadata Pagination { get; set; } = new();
}
