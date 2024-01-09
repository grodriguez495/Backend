using AirQualityControlAPI.Domain.Enums;
using MediatR;

namespace AirQualityControlAPI.Application.Features.Sensors.Queries.GetSensorByDateAndVariables;

public class GetSensorByDateAndVariableQuery: IRequest<List<SensorDto>>
{
    public int VariableId { get; set; }
    public int InputDate { get; set; }

    public GetSensorByDateAndVariableQuery(int variableId, int inputDate)
    {
        VariableId = variableId;
        InputDate = inputDate;
    }
}