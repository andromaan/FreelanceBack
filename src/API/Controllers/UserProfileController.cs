using API.Controllers.Common;
using BLL.Commands.UserProfiles;
using Domain;
using Domain.ViewModels.UserProfile;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(Roles = $"{Settings.Roles.AdminRole}, {Settings.Roles.FreelancerRole}")]
public class UserProfileController(ISender sender) : BaseController
{
    [HttpPut("{id}")]
    public virtual async Task<IActionResult> Update(Guid userId, UpdateProfileVM vm, CancellationToken ct)
        => GetResult(await sender.Send(new UpdateProfileCommand(userId, vm), ct));
}