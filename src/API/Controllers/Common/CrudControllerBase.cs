using AutoMapper;
using BLL.Common;
using BLL.Services;
using Domain.Common.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Common;

[ApiController]
[Route("api/[controller]")]
public abstract class CrudControllerBase<TKey, TEntity, TCreateVm, TUpdateVm, TVM>(
    ISender sender,
    IQueries<TEntity, TKey> queries,
    IMapper mapper) : BaseController
    where TKey : IEquatable<TKey>
    where TEntity : Entity<TKey>
{
    protected readonly ISender _sender = sender;

    protected abstract IRequest<ServiceResponse> CreateCommand(TCreateVm vm);
    protected abstract IRequest<ServiceResponse> UpdateCommand(TKey id, TUpdateVm vm);
    protected abstract IRequest<ServiceResponse> DeleteCommand(TKey id);

    [HttpGet]
    public virtual async Task<IActionResult> GetAll(CancellationToken ct)
        => GetResult(ServiceResponse.OkResponse("Entities retrieved successfully",
            mapper.Map<IEnumerable<TVM>>(await queries.GetAllAsync(token: ct))));

    [HttpGet("{id}")]
    public virtual async Task<IActionResult> GetById(TKey id, CancellationToken ct)
        => GetResult(ServiceResponse.OkResponse("Entity retrieved successfully",
            await queries.GetByIdAsync(id, token: ct)));

    [HttpPost]
    public virtual async Task<IActionResult> Create(TCreateVm vm, CancellationToken ct)
        => GetResult(await _sender.Send(CreateCommand(vm), ct));

    [HttpPut("{id}")]
    public virtual async Task<IActionResult> Update(TKey id, TUpdateVm vm, CancellationToken ct)
        => GetResult(await _sender.Send(UpdateCommand(id, vm), ct));

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete(TKey id, CancellationToken ct)
        => GetResult(await _sender.Send(DeleteCommand(id), ct));
}