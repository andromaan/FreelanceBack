using API.Controllers.Common;
using BLL;
using BLL.ViewModels;
using BLL.ViewModels.Country;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(Roles = Settings.Roles.AdminRole)]
public class CountryController(ISender sender)
    : GenericCrudController<int, CountryVM, CreateCountryVM, UpdateCountryVM>(sender)
{
    [AllowAnonymous]
    public override Task<IActionResult> GetAll(CancellationToken ct)
        => base.GetAll(ct);

    [AllowAnonymous]
    public override Task<IActionResult> GetById(int id, CancellationToken ct)
        => base.GetById(id, ct);

    [AllowAnonymous]
    public override Task<IActionResult> GetAllPaginated(PagedVM pagedVm, CancellationToken ct)
        => base.GetAllPaginated(pagedVm, ct);
}