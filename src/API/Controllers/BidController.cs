using API.Controllers.Common;
using BLL.Commands.Bids;
using BLL.ViewModels.Bid;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(Roles = Settings.Roles.AdminOrFreelancer)]
public class BidController(ISender sender)
    : GenericCrudController<Guid, BidVM, CreateBidVM, UpdateBidVM>(sender)
{
    public override async Task<IActionResult> Create(CreateBidVM vm, CancellationToken ct)
    {
        var command = new CreateBidCommand(vm);
        var result = await Sender.Send(command, ct);
        return GetResult(result);
    }

    [AllowAnonymous]
    [HttpGet("by-project/{projectId}")]
    public async Task<IActionResult> GetByProjectId(Guid projectId, CancellationToken ct)
    {
        var query = new GetBidsByProjectIdQuery { ProjectId = projectId };
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