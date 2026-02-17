using API.Controllers.Common;
using BLL;
using BLL.Commands;
using BLL.Commands.Projects;
using BLL.ViewModels;
using BLL.ViewModels.Project;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(Policy = Settings.Roles.AdminOrEmployer)]
public class ProjectController(ISender sender)
    : GenericCrudController<Guid, ProjectVM, CreateProjectVM, UpdateProjectVM>(sender)
{
    [HttpPatch("categories/{projectId}")]
    public async Task<IActionResult> UpdateProjectCategories(Guid projectId, [FromBody] UpdateProjectCategoriesVM vm,
        CancellationToken ct)
    {
        var command = new Update.Command<UpdateProjectCategoriesVM, Guid>
        {
            Id = projectId,
            Model = vm
        };
        var result = await Sender.Send(command, ct);
        return GetResult(result);
    }

    [HttpGet("by-employer")]
    public async Task<IActionResult> GetProjectsByEmployer(CancellationToken ct)
    {
        var query = new GetProjectsByEmployerQuery();
        var result = await Sender.Send(query, ct);
        return GetResult(result);
    }

    [AllowAnonymous]
    public override Task<IActionResult> GetAll(CancellationToken ct)
        => base.GetAll(ct);

    [AllowAnonymous]
    public override Task<IActionResult> GetById(Guid id, CancellationToken ct)
        => base.GetById(id, ct);
    
    [AllowAnonymous]
    public override Task<IActionResult> GetAllPaginated(PagedVM pagedVm, CancellationToken ct)
        => base.GetAllPaginated(pagedVm, ct);
}