using API.Controllers.Common;
using BLL.Commands.Quotes;
using BLL.ViewModels.Quote;
using Domain;
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
    public override async Task<IActionResult> Create(CreateQuoteVM vm, CancellationToken ct)
    {
        var command = new CreateQuoteCommand(vm);
        var result = await Sender.Send(command, ct);
        return GetResult(result);
    }

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
    {
        throw new NotImplementedException();
    }
}