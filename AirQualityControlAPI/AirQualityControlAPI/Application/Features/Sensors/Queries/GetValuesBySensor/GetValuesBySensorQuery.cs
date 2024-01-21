
using MediatR;

namespace AirQualityControlAPI.Application.Features.Sensors.Queries.GetValuesBySensor;

public class GetValuesBySensorQuery : IRequest<List<SensorValuesDto>>
{
    public string Sensor { get; set; }

    public GetValuesBySensorQuery(string sensor)
    {
        Sensor = sensor;
    }
}