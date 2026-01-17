using API.Controllers.Common;
using BLL.Commands.Freelancers;
using Domain;
using Domain.ViewModels.Freelancer;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(Roles = $"{Settings.Roles.AdminRole}, {Settings.Roles.FreelancerRole}")]
public class FreelancerController(ISender sender) : BaseController
{
    [HttpGet]
    public virtual async Task<IActionResult> GetByUser(CancellationToken ct)
        => GetResult(await sender.Send(new GetFreelancerByUserQuery(), ct));
    
    [HttpPut]
    public virtual async Task<IActionResult> Update(UpdateFreelancerVM vm, CancellationToken ct)
        => GetResult(await sender.Send(new UpdateFreelancerCommand(vm), ct));
    
    [HttpPut("languages")]
    public virtual async Task<IActionResult> UpdateLanguages(UpdateFrInfoLangVM vm, CancellationToken ct)
        => GetResult(await sender.Send(new UpdateFrInfoLangCommand(vm), ct));
}