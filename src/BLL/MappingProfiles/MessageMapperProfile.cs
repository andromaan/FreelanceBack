using AutoMapper;
using BLL.ViewModels.Message;
using Domain.Models.Messaging;

namespace BLL.MappingProfiles;

public class MessageMapperProfile : Profile
{
    public MessageMapperProfile()
    {
        CreateMap<Message, MessageVM>()
            .ForMember(dest => dest.SenderId,
                opt => opt.MapFrom(src => src.CreatedBy));

        CreateMap<CreateMessageVM, Message>().ReverseMap();
        CreateMap<CreateMessageWithoutContractVM, Message>().ReverseMap();
        CreateMap<UpdateMessageVM, Message>().ReverseMap();
    }
}