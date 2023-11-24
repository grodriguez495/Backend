using System.Text.Json.Serialization;
using MediatR;

namespace AirQualityControlAPI.Application.Features.Sensors.Queries.GetSensors;

public class GetSensorVariablesQuery: IRequest<List<SensorDto>>
{
    [JsonConstructor]
    public GetSensorVariablesQuery()
    {
    }
}