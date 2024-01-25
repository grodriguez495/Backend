using MediatR;

namespace AirQualityControlAPI.Application.Features.Sensors.Queries.GetSensorValuesByDateAndVariableAndSensor;

public class GetSensorValuesByDateAndVariableAndSensorQuery  : IRequest<List<SensorValuesDto>>
{
   
    public string DateFrom { get; set; }
    public string DateTo { get; set; }
    public int VariableId { get; set; }
    public string Sensor { get; set; }

    public GetSensorValuesByDateAndVariableAndSensorQuery(string dateFrom,string dateTo,int variableId,string sensor)
    {
        DateFrom = dateFrom;
        DateTo = dateTo;
        VariableId = variableId;
        Sensor = sensor;
    }
}