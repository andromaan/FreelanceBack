using API.Controllers.Common;
using BLL.Commands.ContractMilestones;
using BLL.ViewModels.ContractMilestone;
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
[Authorize(Policy = Settings.Roles.AdminOrEmployer)]
public class ContractMilestoneController(ISender sender)
    : GenericCrudController<Guid, ContractMilestoneVM, CreateContractMilestoneVM, UpdateContractMilestoneVM>(sender)
{
    [AllowAnonymous]
    [HttpGet("by-contract/{contractId}")]
    public async Task<IActionResult> GetByContractId(Guid contractId, CancellationToken ct)
    {
        var query = new GetContractMilestonesByContractIdQuery { ContractId = contractId };
        var result = await Sender.Send(query, ct);
        return GetResult(result);
    }
    
    [HttpGet("milestone-status-enums")]
    public IActionResult GetPlatformsAsync()
    {
        var platforms = Enum.GetValues<ContractMilestoneStatus>()
            .Select(x => new { Name = x.ToString(), Value = (int)x })
            .ToList();

        return Ok(platforms);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<IActionResult> GetAll(CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}