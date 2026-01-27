using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.UserWallets;
using DAL.Data;
using Domain.Models.Auth.Users;

namespace DAL.Repositories;

public class UserWalletRepository(AppDbContext context, IUserProvider userProvider)
    : Repository<UserWallet, Guid>(context, userProvider), IUserWalletRepository, IUserWalletQueries
{
}