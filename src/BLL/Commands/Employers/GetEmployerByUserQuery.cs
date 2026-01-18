using AutoMapper;
using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Employers;
using BLL.Services;
using BLL.ViewModels.Employer;
using Domain.Common.Interfaces;
using MediatR;

namespace BLL.Commands.Employers;

public class GetEmployerByUserQuery : IRequest<ServiceResponse>
{
}

public class QueryHandler(
    IEmployerQueries queriesEmployer,
    IUserProvider userProvider,
    IMapper mapper)
    : IRequestHandler<GetEmployerByUserQuery, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(GetEmployerByUserQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var userId = await userProvider.GetUserId();

            var employer = await queriesEmployer.GetByUserId(userId, cancellationToken, includes: true);
            if (employer == null)
            {
                return ServiceResponse.NotFound("Employer not found");
            }

            return ServiceResponse.Ok("Employer retrieved",
                mapper.Map<EmployerVM>(employer));
        }
        catch (Exception exception)
        {
            return ServiceResponse.InternalError(exception.Message);
        }
    }
}