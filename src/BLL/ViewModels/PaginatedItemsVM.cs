namespace BLL.ViewModels;

public class PaginatedItemsVM<T>
    where T: class
{
    public int Page { get; set; }
    public int PageCount { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public List<T> Items { get; set; } = [];
}