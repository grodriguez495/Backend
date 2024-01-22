using System.Runtime.CompilerServices;
using AirQualityControlAPI.Application.Features.Notifications;
using AirQualityControlAPI.Application.Features.Notifications.Commands.Delete;
using AirQualityControlAPI.Application.Features.Notifications.Queries;
using AirQualityControlAPI.Application.Features.Notifications.Queries.GetActiveNotifications;
using AirQualityControlAPI.Application.Features.Notifications.Queries.GetNotificationsByEmailOrPhone;
using AirQualityControlAPI.Application.Features.Users.Commands.DeleteUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AirQualityControlAPI.Api.Controllers;

public class NotificationController : BaseController
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
            return await _mediator.Send(new GetNotificationByEmailOrPhoneQuery(email, phone));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new List<NotificationDto>();
        }

    }

    [HttpGet("Active-notification")]
    public async Task<List<NotificationDto>> GetActiveAlertsByEmailOrPhoneNumber(string email, string phone)
    {
        try
        {
            return await _mediator.Send(new GetActiveNotificationQuery(email, phone));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new List<NotificationDto>();
        }
    }

    [HttpDelete("{id:required}")] 
    public async Task<ActionResult<bool>> Delete(int id) => 
        await _mediator.Send(new DeleteNotificationCommand() { Id = id });

}