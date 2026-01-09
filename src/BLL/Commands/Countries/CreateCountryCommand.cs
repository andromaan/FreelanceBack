using AutoMapper;
using BLL.Common.Interfaces.Repositories.Countries;
using BLL.Services;
using Domain.Models.Countries;
using Domain.ViewModels.Country;
using MediatR;

namespace BLL.Commands.Countries;

public record CreateCountryCommand : IRequest<ServiceResponse>
{
    public required CreateCountryVM Country { get; init; }
}

public record CreateCountryCommandHandler(
    ICountryRepository CountryRepository,
    ICountryQueries CountryQueries,
    IMapper Mapper
    ) : IRequestHandler<CreateCountryCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var createCountryModel = Mapper.Map<Country>(request.Country);

            var createdCountry = await CountryRepository.CreateAsync(createCountryModel, cancellationToken);
            return ServiceResponse.OkResponse("Country created", createdCountry);
        }
        catch (Exception exception)
        {
            return ServiceResponse.InternalServerErrorResponse(exception.Message);
        }
    }
}