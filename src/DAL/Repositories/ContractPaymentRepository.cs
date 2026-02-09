using BLL.Common.Interfaces;
using BLL.Common.Interfaces.Repositories.ContractPayments;
using DAL.Data;
using Domain.Models.Payments;

namespace DAL.Repositories;

public class ContractPaymentRepository(AppDbContext context, IUserProvider userProvider)
    : Repository<ContractPayment, Guid>(context, userProvider), IContractPaymentRepository, IContractPaymentQueries
{
}