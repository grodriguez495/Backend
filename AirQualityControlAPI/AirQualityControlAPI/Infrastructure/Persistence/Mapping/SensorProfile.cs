using AirQualityControlAPI.Application.Features.Sensors.Queries;
using AirQualityControlAPI.Application.Features.Sensors.Queries.GetSensorIds;
using AirQualityControlAPI.Domain.Models;
using AutoMapper;

namespace AirQualityControlAPI.Infrastructure.Persistence.Mapping;

public class SensorProfile:Profile
{
   public SensorProfile()
   {
       CreateMap<Sensor, SensorDto>();
   }
}