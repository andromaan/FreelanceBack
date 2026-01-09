using API.Controllers.Common;
using AutoMapper;
using BLL.Commands.Countries;
using BLL.Common.Interfaces.Repositories.Countries;
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
public class CountryController(ISender sender, ICountryQueries countriesQueries, IMapper mapper)
    : CrudControllerBase<int, Country, CreateCountryVM, UpdateCountryVM, CountryVM>(sender, countriesQueries, mapper)
{
    protected override IRequest<ServiceResponse> CreateCommand(CreateCountryVM vm)
        => new CreateCountryCommand { Country = vm };

    protected override IRequest<ServiceResponse> UpdateCommand(int id, UpdateCountryVM vm)
        => new UpdateCountryCommand { Id = id, Country = vm };

    protected override IRequest<ServiceResponse> DeleteCommand(int id)
        => new DeleteCountryCommand { Id = id };
}