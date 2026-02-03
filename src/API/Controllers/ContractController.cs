using API.Controllers.Common;
using BLL.Commands;
using BLL.Commands.Contracts;
using BLL.ViewModels.Contract;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ContractController(ISender sender) : BaseController
{
    [Authorize(Roles = Settings.Roles.EmployerRole)]
    [HttpPost("{quoteId:guid}")]
    public async Task<IActionResult> CreateContract([FromRoute] Guid quoteId, CancellationToken ct)
    {
        var command = new CreateContractCommand { QuoteId = quoteId };
        var result = await sender.Send(command, ct);
        return GetResult(result);
    }

    [Authorize(Roles = Settings.Roles.EmployerRole)]
    [HttpPut]
    public async Task<IActionResult> UpdateContract(Guid contractId, UpdateContractVM vm, CancellationToken ct)
    {
        var command = new Update.Command<UpdateContractVM, Guid> { Id = contractId, Model = vm };
        var result = await sender.Send(command, ct);
        return GetResult(result);
    }
}