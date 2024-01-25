using System.Globalization;
using AirQualityControlAPI.Domain.Enums;
using AirQualityControlAPI.Domain.Repositories.Alerts.Queries;
using MediatR;

namespace AirQualityControlAPI.Application.Features.Notifications.Queries.GetNotificationsByEmailOrPhonePerDay;

public class GetNotificationsByEmailOrPhonePerDayQueryHandler : IRequestHandler<GetNotificationsByEmailOrPhonePerDayQuery, List<NotificationDto>>
{
    private readonly IAlertsQueryRepository _alertsQueryRepository;
    public GetNotificationsByEmailOrPhonePerDayQueryHandler(IAlertsQueryRepository alertsQueryRepository)
    {
        _alertsQueryRepository = alertsQueryRepository ?? throw new ArgumentNullException(nameof(alertsQueryRepository));
    }

    public async Task<List<NotificationDto>> Handle(GetNotificationsByEmailOrPhonePerDayQuery request, CancellationToken cancellationToken)
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

           
            var baseDate = DateTime.Now;
            var dateFrom = new DateTime(baseDate.Year, baseDate.Month, baseDate.Day, 0, 0, 1).ToString("dd-MM-yyyyTHH:mm:ss");
            var dateTo = new DateTime(baseDate.Year, baseDate.Month, baseDate.Day, 23, 59, 59).ToString("dd-MM-yyyyTHH:mm:ss");
            var dateFromFinal = DateTime.ParseExact(dateFrom,  "dd-MM-yyyyTHH:mm:ss",CultureInfo.InvariantCulture);
            var dateToFinal =  DateTime.ParseExact(dateTo,  "dd-MM-yyyyTHH:mm:ss",CultureInfo.InvariantCulture);

            var result = new List<NotificationDto>();
            var entities = await _alertsQueryRepository.ListAsync(x
                    => availableRecipients.Contains(x.Recipient)
                , false,
                cancellationToken);
            var entityList = entities.Where(x=> x.Timestamp >= dateFromFinal &&
                                                x.Timestamp <= dateToFinal ).ToList();
            foreach (var eachResult in entityList)
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