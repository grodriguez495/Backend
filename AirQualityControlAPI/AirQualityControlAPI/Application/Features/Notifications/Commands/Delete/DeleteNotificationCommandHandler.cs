using AirQualityControlAPI.Domain.Repositories.Alerts.Commands;
using AirQualityControlAPI.Domain.Repositories.Alerts.Queries;
using MediatR;

namespace AirQualityControlAPI.Application.Features.Notifications.Commands.Delete;

public class DeleteNotificationCommandHandler : IRequestHandler<DeleteNotificationCommand,bool>
{
    private readonly IAlertsQueryRepository _alertsQueryRepository;
    private readonly IAlertsCommandRepository _alertsCommandRepository;
    public DeleteNotificationCommandHandler(IAlertsQueryRepository alertsQueryRepository, IAlertsCommandRepository alertsCommandRepository)
    {
        _alertsQueryRepository = alertsQueryRepository ?? throw new ArgumentNullException(nameof(alertsQueryRepository));
        _alertsCommandRepository = alertsCommandRepository ?? throw new ArgumentNullException(nameof(alertsCommandRepository));
    }

    public async Task<bool> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var currentNotification = await _alertsQueryRepository.FindAsync(request.Id, true, cancellationToken);
            currentNotification.IsOpened = !currentNotification.IsOpened;
            return await _alertsCommandRepository.EditAsync(currentNotification, cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }
    }
}