using API.Controllers.Common;
using BLL.Commands.Projects;
using BLL.ViewModels.Project;
using Domain;
using Domain.Models.Projects;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(Roles = $"{Settings.Roles.AdminRole}, {Settings.Roles.EmployerRole}")]
public class ProjectController(ISender sender)
    : GenericCrudController<Guid, ProjectVM, CreateProjectVM, UpdateProjectVM>(sender)
{
    public override async Task<IActionResult> Create(CreateProjectVM vm, CancellationToken ct)
    {
        var command = new CreateProjectCommand(vm);
        var result = await Sender.Send(command, ct);
        return GetResult(result);
    }
}