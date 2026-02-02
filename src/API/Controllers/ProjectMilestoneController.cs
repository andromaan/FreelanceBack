using API.Controllers.Common;
using BLL.Commands.ProjectMilestones;
using BLL.ViewModels.ProjectMilestone;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(Policy = Settings.Roles.AdminOrEmployer)]
public class ProjectMilestoneController(ISender sender)
    : GenericCrudController<Guid, ProjectMilestoneVM, CreateProjectMilestoneVM, UpdateProjectMilestoneVM>(sender)
{
    public override async Task<IActionResult> Create(CreateProjectMilestoneVM vm, CancellationToken ct)
    {
        var command = new CreateProjectMilestoneCommand(vm);
        var result = await Sender.Send(command, ct);
        return GetResult(result);
    }

    [AllowAnonymous]
    [HttpGet("by-project/{projectId}")]
    public async Task<IActionResult> GetByProjectId(Guid projectId, CancellationToken ct)
    {
        var query = new GetProjectMilestonesByProjectIdQuery { ProjectId = projectId };
        var result = await Sender.Send(query, ct);
        return GetResult(result);
    }
    
    

    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<IActionResult> GetAll(CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}