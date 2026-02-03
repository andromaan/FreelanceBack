using BLL.Common.Interfaces.Repositories.Projects;
using BLL.Common.Validators;
using BLL.Services;
using BLL.ViewModels.Quote;

namespace BLL.Commands.Quotes;

public class CreateQuoteValidator(
    IProjectQueries projectQueries) 
    : ICreateValidator<CreateQuoteVM>
{
    public async Task<ServiceResponse?> ValidateAsync(
        CreateQuoteVM createModel,
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