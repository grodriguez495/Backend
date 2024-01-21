using AirQualityControlAPI.Domain.Models;

namespace AirQualityControlAPI.Domain.Repositories.Alerts.Queries;

public interface IAlertsQueryRepository: IQueryRepository<AlertNotification,int>
{
    
}