using AirQualityControlAPI.Domain.Enums;
using AirQualityControlAPI.Domain.Repositories.Alerts.Queries;
using MediatR;

namespace AirQualityControlAPI.Application.Features.Notifications.Queries.GetActiveNotifications;

public class GetActiveNotificationQueryHandler  : IRequestHandler<GetActiveNotificationQuery, List<NotificationDto>>
{
    private readonly IAlertsQueryRepository _alertsQueryRepository;
    public GetActiveNotificationQueryHandler(IAlertsQueryRepository alertsQueryRepository)
    {
        _alertsQueryRepository = alertsQueryRepository ?? throw new ArgumentNullException(nameof(alertsQueryRepository));
    }

    public async Task<List<NotificationDto>> Handle(GetActiveNotificationQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var availableRecipients = new List<string>();
            if(!string.IsNullOrWhiteSpace(request.Email))
                availableRecipients.Add(request.Email);
            if (!string.IsNullOrWhiteSpace(request.Phone))
            {
                var completeNumber = $"+57{request.Phone}";
                availableRecipients.Add(completeNumber);
            }
            var result = new List<NotificationDto>();
            var entities = await _alertsQueryRepository.ListAsync(x => availableRecipients.Contains(x.Recipient) 
                && !x.IsOpened, false,
                cancellationToken);

            foreach (var eachResult in entities)
            {
                var variableName = Enum.GetName(typeof(AlertTypeEnum), eachResult.AlertType);
                var newNotification = new NotificationDto()
                {
                    AlertId = eachResult.AlertId,
                    Timestamp = eachResult.Timestamp.ToString(),
                    Message = eachResult.Message,
                    Recipient = eachResult.Recipient,
                    AlertType = eachResult.AlertType,
                    AlertTypeName = variableName,
                    IsOpened = eachResult.IsOpened
                };
                result.Add(newNotification);
            }
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new List<NotificationDto>();
        }
    }
}