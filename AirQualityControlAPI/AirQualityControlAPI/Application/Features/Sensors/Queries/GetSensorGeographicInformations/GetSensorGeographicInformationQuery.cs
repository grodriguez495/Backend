using System.Text.Json.Serialization;
using MediatR;

namespace AirQualityControlAPI.Application.Features.Sensors.Queries.GetSensorGeographicInformations;

public class GetSensorGeographicInformationQuery :IRequest<List<SensorGeographicInformationDto>>
{
    [JsonConstructor]
    public GetSensorGeographicInformationQuery()
    {
    }
}