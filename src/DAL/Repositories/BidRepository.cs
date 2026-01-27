using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Bids;
using DAL.Data;
using Domain.Models.Projects;

namespace DAL.Repositories;

public class BidRepository(AppDbContext context, IUserProvider provider)
    : Repository<Bid, Guid>(context, provider), IBidRepository, IBidQueries
{
}