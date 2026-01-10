using API.Controllers.Common;
using AutoMapper;
using BLL.Commands;
using BLL.Services;
using Domain.Models.Countries;
using Domain.ViewModels.Country;
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
        => new GetAll.Query<Country, int, CountryVM>();
    
    protected override IRequest<ServiceResponse> GetByIdQuery(int id) 
        => new GetById.Query<Country, int, CountryVM> { Id = id };
    
    protected override IRequest<ServiceResponse> CreateCommand(CreateCountryVM vm)
        => new Create.Command<CreateCountryVM, Country, int> { Model = vm };

    protected override IRequest<ServiceResponse> UpdateCommand(int id, UpdateCountryVM vm)
        => new Update.Command<UpdateCountryVM, Country, int> { Id = id, Model = vm };

    protected override IRequest<ServiceResponse> DeleteCommand(int id)
        => new Delete.Command<Country, int> { Id = id };
}