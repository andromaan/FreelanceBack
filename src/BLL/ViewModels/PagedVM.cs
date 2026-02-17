using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace BLL.ViewModels;

public class PagedVM
{
    [FromQuery]
    [DefaultValue(1)]
    public int Page { get; set; }
    
    [FromQuery]
    [DefaultValue(10)]
    public int PageSize { get; set; }
}