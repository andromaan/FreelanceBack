using API.Controllers.Common;
using BLL;
using BLL.Commands.Bids;
using BLL.ViewModels;
using BLL.ViewModels.Bid;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(Policy = Settings.Roles.AdminOrFreelancer)]
public class BidController(ISender sender)
    : GenericCrudController<Guid, BidVM, CreateBidVM, UpdateBidVM>(sender)
{
    // TODO Продумати логіку доступу роботодавців і чи можуть не авторизовані користувачі бачити заявки
    [AllowAnonymous]
    [HttpGet("by-project/{projectId}")]
    public async Task<IActionResult> GetByProjectId(Guid projectId, CancellationToken ct)
    {
        var query = new GetBidsByProjectIdQuery { ProjectId = projectId };
        var result = await Sender.Send(query, ct);
        return GetResult(result);
    }
    
    [AllowAnonymous]
    public override async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        => await base.GetById(id, ct);

    [ApiExplorerSettings(IgnoreApi = true)]
    public override async Task<IActionResult> GetAll(CancellationToken ct)
        => await Task.FromResult<IActionResult>(NotFound());

    [ApiExplorerSettings(IgnoreApi = true)]
    public override async Task<IActionResult> GetAllPaginated(PagedVM pagedVm, CancellationToken ct)
        => await Task.FromResult<IActionResult>(NotFound());
}