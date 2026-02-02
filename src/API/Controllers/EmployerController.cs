using API.Controllers.Common;
using BLL.Commands.Employers;
using BLL.ViewModels.Employer;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(Policy = Settings.Roles.AdminOrEmployer)]
public class EmployerController(ISender sender) : BaseController
{
    [HttpGet]
    public virtual async Task<IActionResult> GetByUser(CancellationToken ct)
        => GetResult(await sender.Send(new GetEmployerByUserQuery(), ct));
    
    [HttpPut]
    public virtual async Task<IActionResult> Update(UpdateEmployerVM vm, CancellationToken ct)
        => GetResult(await sender.Send(new UpdateEmployerCommand(vm), ct));
}