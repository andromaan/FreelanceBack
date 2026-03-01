using AutoMapper;
using BLL.ViewModels.Notification;
using Domain.Models.Notifications;

namespace BLL.MappingProfiles;

public class NotificationMapperProfile : Profile
{
    public NotificationMapperProfile()
    {
        CreateMap<Notification, NotificationVM>().ReverseMap();
    }
}