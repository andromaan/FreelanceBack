using API.Controllers.Common;
using BLL.Commands;
using BLL.Commands.Contracts;
using BLL.ViewModels.Contract;
using Domain;
using Domain.Models.Freelance;
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
    
    [HttpGet("status-enums")]
    public IActionResult GetPlatformsAsync()
    {
        var platforms = Enum.GetValues<ContractStatus>()
            .Select(x => new { Name = x.ToString(), Value = (int)x })
            .ToList();

        return Ok(platforms);
    }

    [Authorize(Roles = Settings.Roles.EmployerRole)]
    [HttpPut]
    public async Task<IActionResult> UpdateContract(Guid contractId, UpdateContractVM vm, CancellationToken ct)
    {
        var command = new Update.Command<UpdateContractVM, Guid> { Id = contractId, Model = vm };
        var result = await sender.Send(command, ct);
        return GetResult(result);
    }

    [Authorize(Roles = Settings.Roles.EmployerRole)]
    [HttpPut("update-status/{contractId:guid}")]
    public async Task<IActionResult> UpdateContractStatus(Guid contractId, UpdateContractStatusVM vm,
        CancellationToken ct)
    {
        var command = new Update.Command<UpdateContractStatusVM, Guid> { Id = contractId, Model = vm };
        var result = await sender.Send(command, ct);
        return GetResult(result);
    }
}