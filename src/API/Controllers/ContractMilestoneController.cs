using API.Controllers.Common;
using BLL.Commands;
using BLL.Commands.ContractMilestones;
using BLL.ViewModels.ContractMilestone;
using Domain;
using Domain.Models.Contracts;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
    
    [HttpGet("status-freelancer-enums")]
    public IActionResult GetFreelancerStatusEnumsAsync()
    {
        var platforms = Enum.GetValues<ContractMilestoneFreelancerStatus>()
            .Select(x => new { Name = x.ToString(), Value = (int)x })
            .ToList();

        return Ok(platforms);
    }
    
    [Authorize(Roles = Settings.Roles.FreelancerRole)]
    [HttpPut("status/{id:guid}/freelancer")]
    public async Task<IActionResult> UpdateContractMilestoneStatusForFreelancer(
        Guid id,
        [FromBody] UpdContractMilestoneStatusFreelancerVM vm,
        CancellationToken ct)
    {
        var command = new Update.Command<UpdContractMilestoneStatusFreelancerVM, Guid>
        {
            Id = id,
            Model = vm
        };
        var result = await Sender.Send(command, ct);
        return GetResult(result);
    }
    
    [HttpGet("status-employer-enums")]
    public IActionResult GetEmployerStatusEnumsAsync()
    {
        var platforms = Enum.GetValues<ContractMilestoneEmployerStatus>()
            .Select(x => new { Name = x.ToString(), Value = (int)x })
            .ToList();

        return Ok(platforms);
    }
    
    [Authorize(Roles = Settings.Roles.EmployerRole)]
    [HttpPut("status/{id:guid}/employer")]
    public async Task<IActionResult> UpdateContractMilestoneStatusForEmployer(
        Guid id,
        [FromBody] UpdContractMilestoneStatusEmployerVM vm,
        CancellationToken ct)
    {
        var command = new Update.Command<UpdContractMilestoneStatusEmployerVM, Guid>
        {
            Id = id,
            Model = vm
        };
        var result = await Sender.Send(command, ct);
        return GetResult(result);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<IActionResult> GetAll(CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}