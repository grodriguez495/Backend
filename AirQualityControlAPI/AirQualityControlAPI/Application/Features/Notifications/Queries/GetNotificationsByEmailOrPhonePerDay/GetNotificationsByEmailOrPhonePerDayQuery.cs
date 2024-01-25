using MediatR;

namespace AirQualityControlAPI.Application.Features.Notifications.Queries.GetNotificationsByEmailOrPhonePerDay;

public class GetNotificationsByEmailOrPhonePerDayQuery  : IRequest<List<NotificationDto>>
{
    public string Email { get;set;}
    public string Phone { get; set; }

    public GetNotificationsByEmailOrPhonePerDayQuery(string email ,string phone)
    {
        Email = email;
        Phone = phone;
    }
}