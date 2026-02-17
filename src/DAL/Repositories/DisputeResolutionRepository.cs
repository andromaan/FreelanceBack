using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.DisputeResolutions;
using DAL.Data;
using Domain.Models.Disputes;

namespace DAL.Repositories;

public class DisputeResolutionRepository(AppDbContext context, IUserProvider userProvider)
    : Repository<DisputeResolution, Guid>(context, userProvider), IDisputeResolutionRepository, IDisputeResolutionQueries
{
}