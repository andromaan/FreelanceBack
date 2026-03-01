using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace BLL.ViewModels;

public class PagedVM
{
    [FromQuery]
    [DefaultValue(1)]
    public int Page { get; set; } = 1;
    
    [FromQuery]
    [DefaultValue(10)]
    public int PageSize { get; set; } = 10;
}