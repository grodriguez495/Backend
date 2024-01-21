using System.Text.Json.Serialization;
using MediatR;

namespace AirQualityControlAPI.Application.Features.Sensors.Queries.GetSensorIds;

public class GetSensorIdsQuery:IRequest<List<string>>
{
    [JsonConstructor]
    public GetSensorIdsQuery()
    {
    }
}