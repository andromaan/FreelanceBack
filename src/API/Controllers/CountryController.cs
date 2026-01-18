using API.Controllers.Common;
using AutoMapper;
using BLL.Commands;
using BLL.Services;
using BLL.ViewModels.Country;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
// [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class CountryController(ISender sender, IMapper mapper)
    : CrudControllerBase<int, CreateCountryVM, UpdateCountryVM>(sender, mapper)
{
    protected override IRequest<ServiceResponse> GetAllQuery()
        => new GetAll.Query<CountryVM>();
    
    protected override IRequest<ServiceResponse> GetByIdQuery(int id) 
        => new GetById.Query<int, CountryVM> { Id = id };
    
    protected override IRequest<ServiceResponse> CreateCommand(CreateCountryVM vm)
        => new Create.Command<CreateCountryVM> { Model = vm };

    protected override IRequest<ServiceResponse> UpdateCommand(int id, UpdateCountryVM vm)
        => new Update.Command<UpdateCountryVM, int> { Id = id, Model = vm };

    protected override IRequest<ServiceResponse> DeleteCommand(int id)
        => new Delete.Command<CountryVM, int> { Id = id };
}