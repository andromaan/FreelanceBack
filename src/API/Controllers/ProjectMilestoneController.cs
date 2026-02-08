using API.Controllers.Common;
using BLL.Commands.ProjectMilestones;
using BLL.ViewModels.ProjectMilestone;
using Domain;
using Domain.Models.Projects;
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
    [AllowAnonymous]
    [HttpGet("by-project/{projectId}")]
    public async Task<IActionResult> GetByProjectId(Guid projectId, CancellationToken ct)
    {
        var query = new GetProjectMilestonesByProjectIdQuery { ProjectId = projectId };
        var result = await Sender.Send(query, ct);
        return GetResult(result);
    }
    
    [HttpGet("milestone-status-enums")]
    public IActionResult GetPlatformsAsync()
    {
        var platforms = Enum.GetValues<ProjectMilestoneStatus>()
            .Select(x => new { Name = x.ToString(), Value = (int)x })
            .ToList();

        return Ok(platforms);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<IActionResult> GetAll(CancellationToken ct)
        => Task.FromResult<IActionResult>(NotFound());
}