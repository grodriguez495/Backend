using AirQualityControlAPI.Domain.Models;

namespace AirQualityControlAPI.Domain.Repositories.Sensors.Commands;

public interface ISensorCommandRepository:
        ICreateCommandRepository<Sensor,string>,
        IUpdateCommandRepository<Sensor,string>
{
    
}