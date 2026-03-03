using AutoMapper;
using BLL.ViewModels.Wallet;
using Domain.Models.Payments;

namespace BLL.MappingProfiles;

public class UserWalletMapperProfile : Profile
{
    public UserWalletMapperProfile()
    {
        CreateMap<UserWallet, UserWalletVM>();
    }
}