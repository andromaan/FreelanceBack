using API.Controllers.Common;
using BLL.Commands;
using BLL.Commands.Contracts;
using BLL.ViewModels.Message;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class MessageController(ISender sender)
    : GenericCrudController<Guid, MessageVM, CreateMessageVM, UpdateMessageVM>(sender)
{
    [HttpPost("without-contract")]
    public async Task<IActionResult> CreateWithoutContract(CreateMessageWithoutContractVM vm, CancellationToken ct)
    {
        var command = new Create.Command<CreateMessageWithoutContractVM> { Model = vm };
        var result = await Sender.Send(command, ct);
        return GetResult(result);
    }
    
    [HttpGet("by-user")]
    public async Task<IActionResult> GetProjectsByEmployer(CancellationToken ct)
    {
        var query = new GetMessagesByUserQuery();
        var result = await Sender.Send(query, ct);
        return GetResult(result);
    }
    
    // [HttpGet("by-contract/{contractId}")]
    // public async Task<IActionResult> GetProjectsByEmployer(Guid contractId, CancellationToken ct)
    // {
    //     var query = new GetMessagesByContract { ContractId = contractId };
    //     var result = await Sender.Send(query, ct);
    //     return GetResult(result);
    // }
}