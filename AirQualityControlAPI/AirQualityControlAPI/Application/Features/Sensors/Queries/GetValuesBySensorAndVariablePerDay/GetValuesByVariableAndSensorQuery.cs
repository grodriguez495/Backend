using MediatR;

namespace AirQualityControlAPI.Application.Features.Sensors.Queries.GetValuesBySensorAndVariablePerDay;

public class GetValuesByVariableAndSensorQuery  : IRequest<List<ValuesBySensorAndVariableDto>>
{
    public int VariableId { get; set; }
    public string Sensor { get; set; }

    public GetValuesByVariableAndSensorQuery(int variableId,string sensor)
    {
        VariableId = variableId;
        Sensor = sensor;
    }
}