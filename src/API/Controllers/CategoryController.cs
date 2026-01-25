using API.Controllers.Common;
using BLL.ViewModels.Category;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
// [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
// [Authorize(Roles = Settings.Roles.AdminRole)]
public class CategoryController(ISender sender)
    : GenericCrudController<int, CategoryVM, CreateCategoryVM, UpdateCategoryVM>(sender)
{
}