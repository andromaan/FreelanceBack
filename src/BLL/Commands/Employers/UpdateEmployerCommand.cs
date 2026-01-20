using AutoMapper;
using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Employers;
using BLL.Common.Interfaces.Repositories.Users;
using BLL.Services;
using BLL.ViewModels.Employer;
using MediatR;

namespace BLL.Commands.Employers;

public record UpdateEmployerCommand(UpdateEmployerVM Vm) : IRequest<ServiceResponse>
{
}

public class UpdateEmployerCommandHandler(
    IUserQueries userQueries,
    IEmployerRepository employerRepository,
    IEmployerQueries employerQueries,
    IMapper mapper,
    IUserProvider userProvider)
    : IRequestHandler<UpdateEmployerCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(UpdateEmployerCommand request, CancellationToken cancellationToken)
    {
        var userId = await userProvider.GetUserId();
        var vm = request.Vm;

        var existingUser = await userQueries.GetByIdAsync(userId, cancellationToken);
        if (existingUser == null)
        {
            return ServiceResponse.NotFound($"User with id {userId} not found");
        }
        
        var existingEmployer = await employerQueries.GetByUserId(userId, cancellationToken);
        if (existingEmployer == null)
        {
            return ServiceResponse.NotFound($"User with id {userId} is not an employer");
        }
        
        var updatedProfile = mapper.Map(vm, existingEmployer);
        
        try
        {
            await employerRepository.UpdateAsync(updatedProfile, cancellationToken);
            return ServiceResponse.Ok("User profile updated successfully", updatedProfile);
        }
        catch (Exception exception)
        {
            return ServiceResponse.InternalError(exception.Message);
        }
    }
}