using API.Controllers.Common;
using BLL;
using BLL.Commands;
using BLL.Commands.Freelancers;
using BLL.ViewModels.Freelancer;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(Policy = Settings.Roles.AdminOrFreelancer)]
public class FreelancerController(ISender sender) : BaseController
{
    [HttpGet]
    public virtual async Task<IActionResult> GetByUser(CancellationToken ct)
        => GetResult(await sender.Send(new GetFreelancerByUserQuery(), ct));
    
    [HttpGet("{id:guid}")]
    public virtual async Task<IActionResult> GetByUser(Guid id, CancellationToken ct)
        => GetResult(await sender.Send(new GetFreelancerByIdQuery(id), ct));
    
    [HttpGet("{email}")]
    public virtual async Task<IActionResult> GetByUserEmail(string email, CancellationToken ct)
        => GetResult(await sender.Send(new GetFreelancerByEmailQuery(email), ct));

    [HttpPut]
    public virtual async Task<IActionResult> Update(UpdateFreelancerVM vm, CancellationToken ct)
        => GetResult(await sender.Send(new UpdateByUser.Command<UpdateFreelancerVM> { Model = vm }, ct));

    [HttpPut("languages")]
    public virtual async Task<IActionResult> UpdateLanguages(UpdateFreelancerLanguagesVM vm, CancellationToken ct)
        => GetResult(await sender.Send(new UpdateByUser.Command<UpdateFreelancerLanguagesVM> { Model = vm }, ct));

    [HttpPut("skills")]
    public virtual async Task<IActionResult> UpdateSkills(UpdateFreelancerSkillsVM vm, CancellationToken ct)
        => GetResult(await sender.Send(new UpdateByUser.Command<UpdateFreelancerSkillsVM> { Model = vm }, ct));
}