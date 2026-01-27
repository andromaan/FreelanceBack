using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Quotes;
using DAL.Data;
using Domain.Models.Projects;

namespace DAL.Repositories;

public class QuoteRepository(AppDbContext context, IUserProvider provider)
    : Repository<Quote, Guid>(context, provider), IQuoteRepository, IQuoteQueries
{
}