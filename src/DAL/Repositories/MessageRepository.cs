using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.Messages;
using DAL.Data;
using Domain.Models.Messaging;

namespace DAL.Repositories;

public class MessageRepository(AppDbContext appDbContext, IUserProvider userProvider)
    : Repository<Message, Guid>(appDbContext, userProvider), IMessageRepository, IMessageQueries
{
}

