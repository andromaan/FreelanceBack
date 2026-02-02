using API.Controllers.Common;
using BLL.Commands.ContractMilestones;
using BLL.ViewModels.ContractMilestone;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(Policy = Settings.Roles.AdminOrEmployer)]
public class ContractMilestoneController(ISender sender)
    : GenericCrudController<Guid, ContractMilestoneVM, CreateContractMilestoneVM, UpdateContractMilestoneVM>(sender)
{
    public override async Task<IActionResult> Create(CreateContractMilestoneVM vm, CancellationToken ct)
    {
        var command = new CreateContractMilestoneCommand(vm);
        var result = await Sender.Send(command, ct);
        return GetResult(result);
    }

    [AllowAnonymous]
    [HttpGet("by-contract/{contractId}")]
    public async Task<IActionResult> GetByContractId(Guid contractId, CancellationToken ct)
    {
        var query = new GetContractMilestonesByContractIdQuery { ContractId = contractId };
        var result = await Sender.Send(query, ct);
        return GetResult(result);
    }
    
    

    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<IActionResult> GetAll(CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}