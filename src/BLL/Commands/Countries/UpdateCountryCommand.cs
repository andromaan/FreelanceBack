using AutoMapper;
using BLL.Common.Interfaces.Repositories.Countries;
using BLL.Services;
using Domain.ViewModels.Country;
using MediatR;

namespace BLL.Commands.Countries;

public record UpdateCountryCommand : IRequest<ServiceResponse>
{
    public required int Id { get; init; }
    public required UpdateCountryVM Country { get; init; }
}

public record UpdateCountryCommandHandler(
    ICountryRepository CountryRepository,
    IMapper Mapper
    ) : IRequestHandler<UpdateCountryCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var existingCountry = Mapper.Map<Domain.Models.Countries.Country>(request.Country);
            existingCountry.Id = request.Id;
            var updatedCountry = await CountryRepository.UpdateAsync(existingCountry, cancellationToken);
            if (updatedCountry == null)
                return ServiceResponse.NotFoundResponse($"Country with id {request.Id} not found");
            return ServiceResponse.OkResponse("Country updated", updatedCountry);
        }
        catch (Exception exception)
        {
            return ServiceResponse.InternalServerErrorResponse(exception.Message);
        }
    }
}
