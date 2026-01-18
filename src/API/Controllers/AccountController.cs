using API.Controllers.Common;
using BLL.Commands.Auth;
using BLL.ViewModels.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("[controller]")]
[ApiController]
public class AccountController(ISender sender) : BaseController
{
    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] SignUpVm vm, CancellationToken cancellationToken)
    {
        var response = await sender.Send(new SignUpCommand(vm), cancellationToken);
        return GetResult(response);
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] SignInVm vm, CancellationToken cancellationToken)
    {
        var response = await sender.Send(new SignInCommand(vm), cancellationToken);
        return GetResult(response);
    }

    [HttpPost("external-login")]
    public async Task<IActionResult> GoogleExternalLoginAsync([FromBody] ExternalLoginVm model,
        CancellationToken cancellationToken)
    {
        var command = new GoogleExternalLoginCommand { Model = model };
        var result = await sender.Send(command, cancellationToken);
        return GetResult(result);
    }
}