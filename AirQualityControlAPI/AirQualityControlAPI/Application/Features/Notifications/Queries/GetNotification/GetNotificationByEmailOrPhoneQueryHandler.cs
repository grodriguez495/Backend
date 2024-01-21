using AirQualityControlAPI.Domain.Enums;
using AirQualityControlAPI.Domain.Repositories.Alerts.Queries;
using AutoMapper;
using MediatR;

namespace AirQualityControlAPI.Application.Features.Notifications.Queries.GetNotification;

public class GetNotificationByEmailOrPhoneQueryHandler : IRequestHandler<GetNotificationByEmailOrPhoneQuery, List<NotificationDto>>
{
    private readonly IAlertsQueryRepository _alertsQueryRepository;
  
    
    public GetNotificationByEmailOrPhoneQueryHandler(IAlertsQueryRepository alertsQueryRepository)
    {
        _alertsQueryRepository = alertsQueryRepository ?? throw new ArgumentNullException(nameof(alertsQueryRepository));
     }

    public async  Task<List<NotificationDto>> Handle(GetNotificationByEmailOrPhoneQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var availableRecipients = new List<string>();
            if(!string.IsNullOrWhiteSpace(request.Email))
                availableRecipients.Add(request.Email);
            if (!string.IsNullOrWhiteSpace(request.Phone))
                availableRecipients.Add(request.Phone);
            var result = new List<NotificationDto>();
            var entities = await _alertsQueryRepository.ListAsync(x => availableRecipients.Contains(x.Recipient), false,
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
                    AlertTypeName = variableName
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