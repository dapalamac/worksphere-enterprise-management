namespace WorkSphere.Application.Common;

public class PagedData<T>
{
    public List<T> Items { get; set; } = [];

    public int TotalRecords { get; set; }
}
