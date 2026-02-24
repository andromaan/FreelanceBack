using BLL.CommandsQueries.GenericCRUD.GetAll;
using BLL.ViewModels.Notification;
using FluentValidation;

namespace BLL.CommandsQueries.Notifications.FluentValidations;

public class GetAllFilteredNotificationValidation : AbstractValidator<GetAllFilteredPaginated.Query<FilterNotificationVM>>
{
    public GetAllFilteredNotificationValidation()
    {
        RuleFor(f => f.FilteringVm.NotificationType).IsInEnum();
    }
}