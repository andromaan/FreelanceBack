using API.Controllers.Common;
using AutoMapper;
using BLL.Commands;
using BLL.Services;
using BLL.ViewModels.Category;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
// [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class CategoryController(ISender sender, IMapper mapper)
    : CrudControllerBase<Guid, CreateCategoryVM, UpdateCategoryVM>(sender, mapper)
{
    protected override IRequest<ServiceResponse> GetAllQuery()
        => new GetAll.Query<CategoryVM>();

    protected override IRequest<ServiceResponse> GetByIdQuery(Guid id)
        => new GetById.Query<Guid, CategoryVM> { Id = id };

    protected override IRequest<ServiceResponse> CreateCommand(CreateCategoryVM vm)
        => new Create.Command<CreateCategoryVM> { Model = vm };

    protected override IRequest<ServiceResponse> UpdateCommand(Guid id, UpdateCategoryVM vm)
        => new Update.Command<UpdateCategoryVM, Guid> { Id = id, Model = vm };

    protected override IRequest<ServiceResponse> DeleteCommand(Guid id)
        => new Delete.Command<CategoryVM, Guid> { Id = id };
}