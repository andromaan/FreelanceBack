using AutoMapper;
using BLL.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Common;

[ApiController]
[Route("api/[controller]")]
public abstract class CrudControllerBase<TKey, TCreateVm, TUpdateVm>(
    ISender sender,
    IMapper mapper) : BaseController
{
    protected readonly ISender Sender = sender;

    protected abstract IRequest<ServiceResponse> GetAllQuery();
    protected abstract IRequest<ServiceResponse> GetByIdQuery(TKey id);
    protected abstract IRequest<ServiceResponse> CreateCommand(TCreateVm vm);
    protected abstract IRequest<ServiceResponse> UpdateCommand(TKey id,TUpdateVm vm);
    protected abstract IRequest<ServiceResponse> DeleteCommand(TKey id);

    [HttpGet]
    public virtual async Task<IActionResult> GetAll(CancellationToken ct)
        => GetResult(await Sender.Send(GetAllQuery(), ct));

    [HttpGet("{id}")]
    public virtual async Task<IActionResult> GetById(TKey id, CancellationToken ct)
        => GetResult(await Sender.Send(GetByIdQuery(id), ct));

    [HttpPost]
    public virtual async Task<IActionResult> Create(TCreateVm vm, CancellationToken ct)
        => GetResult(await Sender.Send(CreateCommand(vm), ct));

    [HttpPut("{id}")]
    public virtual async Task<IActionResult> Update(TKey id, TUpdateVm vm, CancellationToken ct)
        => GetResult(await Sender.Send(UpdateCommand(id, vm), ct));

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete(TKey id, CancellationToken ct)
        => GetResult(await Sender.Send(DeleteCommand(id), ct));
}