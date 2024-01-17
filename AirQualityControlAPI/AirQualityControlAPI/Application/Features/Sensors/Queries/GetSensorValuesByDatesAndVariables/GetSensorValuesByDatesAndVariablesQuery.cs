using MediatR;

namespace AirQualityControlAPI.Application.Features.Sensors.Queries.GetSensorValuesBydatesAndVariables;

public class GetSensorValuesByDatesAndVariablesQuery : IRequest<List<SensorDto>>
{
    public string DateFrom { get; set; }
    public string DateTo { get; set; }
    public int VariableId { get; set; }

    public GetSensorValuesByDatesAndVariablesQuery(string dateFrom, string dateTo, int variableId)
    {
        VariableId = variableId;
        DateFrom = dateFrom;
        DateTo = dateTo;
    }
}