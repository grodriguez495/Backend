using MediatR;
using Newtonsoft.Json;

namespace AirQualityControlAPI.Application.Features.Sensors.Queries.GetSensorIds;

public class GetSensorIdsQuery:IRequest<List<string>>
{
    [JsonConstructor]
    public GetSensorIdsQuery()
    {
    }
}