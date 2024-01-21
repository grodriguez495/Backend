using AirQualityControlAPI.Domain.Models;

namespace AirQualityControlAPI.Domain.Repositories.Alerts.Commands;

public interface IAlertsCommandRepository:
    ICreateCommandRepository<AlertNotification,int>,
    IUpdateCommandRepository<AlertNotification,int>
{
    
}