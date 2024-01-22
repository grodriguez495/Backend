using MediatR;

namespace AirQualityControlAPI.Application.Features.Notifications.Queries.GetActiveNotifications;

public class GetActiveNotificationQuery : IRequest<List<NotificationDto>>
{
    public string Email { get;set;}
    public string Phone { get; set; }

    public GetActiveNotificationQuery(string email ,string phone)
    {
        Email = email;
        Phone = phone;
    }
}