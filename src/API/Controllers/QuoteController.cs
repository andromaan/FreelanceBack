using API.Controllers.Common;
using BLL;
using BLL.Commands.Quotes;
using BLL.ViewModels;
using BLL.ViewModels.Quote;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(Policy = Settings.Roles.AdminOrFreelancer)]
public class QuoteController(ISender sender)
    : GenericCrudController<Guid, QuoteVM, CreateQuoteVM, UpdateQuoteVM>(sender)
{
    [AllowAnonymous]
    [HttpGet("by-project/{projectId}")]
    public async Task<IActionResult> GetByProjectId(Guid projectId, CancellationToken ct)
    {
        var query = new GetQuotesByProjectIdQuery { ProjectId = projectId };
        var result = await Sender.Send(query, ct);
        return GetResult(result);
    }

    [AllowAnonymous]
    public override Task<IActionResult> GetById(Guid id, CancellationToken ct)
        => base.GetById(id, ct);

    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<IActionResult> GetAll(CancellationToken ct)
        => Task.FromResult<IActionResult>(NotFound());

    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<IActionResult> GetAllPaginated(PagedVM pagedVm, CancellationToken ct)
        => Task.FromResult<IActionResult>(NotFound());
}