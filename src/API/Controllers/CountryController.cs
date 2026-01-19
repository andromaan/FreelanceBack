using API.Controllers.Common;
using BLL.ViewModels.Country;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
// [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
// [Authorize(Roles = Settings.Roles.AdminRole)]
public class CountryController(ISender sender)
    : GenericCrudController<int, CountryVM, CreateCountryVM, UpdateCountryVM>(sender)
{
}