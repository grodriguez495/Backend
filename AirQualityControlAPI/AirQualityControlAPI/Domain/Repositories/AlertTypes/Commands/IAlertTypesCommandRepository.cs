using AirQualityControlAPI.Domain.Models;

namespace AirQualityControlAPI.Domain.Repositories.AlertTypes.Commands;

public interface IAlertTypesCommandRepository:
    ICreateCommandRepository<AlertType,int>,
    IUpdateCommandRepository<AlertType,int>
{
    
}