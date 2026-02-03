using BLL.Common.Interfaces.Repositories.Projects;
using BLL.Common.Validators;
using BLL.Services;
using BLL.ViewModels.Bid;

namespace BLL.Commands.Bids;

public class CreateBidValidator(
    IProjectQueries projectQueries) 
    : ICreateValidator<CreateBidVM>
{
    public async Task<ServiceResponse?> ValidateAsync(
        CreateBidVM createModel,
        CancellationToken cancellationToken)
    {
        var existingProject = await projectQueries.GetByIdAsync(createModel.ProjectId, cancellationToken);
        
        if (existingProject is null)
        {
            return ServiceResponse.NotFound($"Project with Id {createModel.ProjectId} not found");
        }

        return null; // Валідація пройшла успішно
    }
}