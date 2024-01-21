

using AirQualityControlAPI.Application.Features.Notifications;
using AirQualityControlAPI.Application.Features.Notifications.Queries.GetNotification;
using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace AirQualityControlAPI.Api.Controllers;

public class NotificationController: BaseController
{
  public NotificationController(IMediator mediator) : base(mediator)
 {
    _mediator = mediator;
  }

  [HttpGet("By-emil-and-phone")]
  public async Task<List<NotificationDto>> GetAlertsByEmailOrPhoneNumber(string email, string phone)
  {
    try
    {
      return await _mediator.Send(new GetNotificationByEmailOrPhoneQuery(email,phone));
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      return new List<NotificationDto>();
    }

  }
}