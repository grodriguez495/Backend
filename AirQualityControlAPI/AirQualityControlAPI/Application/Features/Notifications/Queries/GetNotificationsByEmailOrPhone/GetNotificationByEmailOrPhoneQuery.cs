﻿using MediatR;

namespace AirQualityControlAPI.Application.Features.Notifications.Queries.GetNotificationsByEmailOrPhone;

public class GetNotificationByEmailOrPhoneQuery : IRequest<List<NotificationDto>>
{
    public string Email { get;set;}
    public string Phone { get; set; }

    public GetNotificationByEmailOrPhoneQuery(string email ,string phone)
    {
        Email = email;
        Phone = phone;
    }
}