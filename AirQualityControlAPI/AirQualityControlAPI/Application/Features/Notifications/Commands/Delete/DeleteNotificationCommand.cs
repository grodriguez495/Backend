using MediatR;

namespace AirQualityControlAPI.Application.Features.Notifications.Commands.Delete;

public class DeleteNotificationCommand: IRequest<bool>
{
    public int Id { get; set; }
}