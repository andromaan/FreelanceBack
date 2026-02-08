using API.Controllers.Common;
using BLL.Commands.Reviews;
using BLL.ViewModels.Reviews;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(Policy = Settings.Roles.AnyAuthenticated)]
public class ReviewController(ISender sender)
    : GenericCrudController<Guid, ReviewVM, CreateReviewVM, UpdateReviewVM>(sender)
{
    [HttpGet("by-reviewed-user/{reviewedUserEmail}")]
    public async Task<IActionResult> GetByGetReviewedUser(string reviewedUserEmail, CancellationToken ct)
    {
        var query = new GetByReviewedUserQuery { ReviewedUserEmail = reviewedUserEmail };
        var result = await Sender.Send(query, ct);
        return GetResult(result);
    }
    
    [HttpGet("by-user")]
    public async Task<IActionResult> GetByGetReviewer(CancellationToken ct)
    {
        var query = new GetByReviewerQuery();
        var result = await Sender.Send(query, ct);
        return GetResult(result);
    }
    
    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<IActionResult> GetAll(CancellationToken ct)
        => Task.FromResult<IActionResult>(NotFound());
}