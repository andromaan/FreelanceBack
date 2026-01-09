using BLL.Common.Interfaces.Repositories.Countries;
using BLL.Services;
using MediatR;

namespace BLL.Commands.Countries;

public record DeleteCountryCommand : IRequest<ServiceResponse>
{
    public required int Id { get; init; }
}

public record DeleteCountryCommandHandler(
    ICountryRepository CountryRepository
    ) : IRequestHandler<DeleteCountryCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var deletedCountry = await CountryRepository.DeleteAsync(request.Id, cancellationToken);
            if (deletedCountry == null)
                return ServiceResponse.NotFoundResponse($"Country with id {request.Id} not found");
            return ServiceResponse.OkResponse("Country deleted");
        }
        catch (Exception exception)
        {
            return ServiceResponse.InternalServerErrorResponse(exception.Message);
        }
    }
}
