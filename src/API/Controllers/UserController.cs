using API.Controllers.Common;
using BLL;
using BLL.Commands;
using BLL.Commands.Users;
using BLL.ViewModels.User;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UserController(ISender sender)
    : GenericCrudController<Guid, UserVM, CreateUserByAdminVM, UpdateUserByAdminVM>(sender)
{
    [HttpGet("get-myself")]
    public async Task<IActionResult> GetMyself(CancellationToken ct)
    {
        var command = new GetUserByTokenQuery();
        var result = await Sender.Send(command, ct);
        return GetResult(result);
    }

    [HttpPatch("update-avatar")]
    public async Task<IActionResult> UpdateAvatar(IFormFile file, CancellationToken ct)
    {
        var command = new UpdateUserAvatarCommand(file);
        var result = await Sender.Send(command, ct);
        return GetResult(result);
    }

    [HttpPut("languages")]
    public virtual async Task<IActionResult> UpdateLanguages(UpdateUserLanguagesVM vm, CancellationToken ct)
        => GetResult(await Sender.Send(new UpdateByUser.Command<UpdateUserLanguagesVM> { Model = vm }, ct));

    [Authorize(Roles = Settings.Roles.AdminRole)]
    [HttpPut("{userId:guid}/languages")]
    public virtual async Task<IActionResult> UpdateLanguages(Guid userId, UpdateUserLanguagesByAdminVM vm,
        CancellationToken ct)
        => GetResult(await Sender.Send(
            new Update.Command<UpdateUserLanguagesByAdminVM, Guid> { Id = userId, Model = vm }, ct));

    [Authorize(Roles = Settings.Roles.AdminRole)]
    public override async Task<IActionResult> Create(CreateUserByAdminVM byAdminVm, CancellationToken ct)
    {
        var command = new CreateUserByAdminCommand(byAdminVm);
        var result = await Sender.Send(command, ct);
        return GetResult(result);
    }

    [Authorize(Roles = Settings.Roles.AdminRole)]
    public override Task<IActionResult> Update(Guid id, UpdateUserByAdminVM byAdminVm, CancellationToken ct)
        => base.Update(id, byAdminVm, ct);

    [Authorize(Roles = Settings.Roles.AdminRole)]
    public override Task<IActionResult> Delete(Guid id, CancellationToken ct)
        => base.Delete(id, ct);

    [Authorize(Roles = Settings.Roles.AdminRole)]
    public override Task<IActionResult> GetAll(CancellationToken ct)
        => base.GetAll(ct);

    [Authorize(Roles = Settings.Roles.AdminRole)]
    public override Task<IActionResult> GetById(Guid id, CancellationToken ct)
        => base.GetById(id, ct);
}