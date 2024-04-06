namespace AirQualityControlAPI.Application.Features.Sensors.Queries.GetValuesBySensorAndVariablePerDay;

public class ValuesBySensorAndVariableDto
{
    public double Value { get; set; }
    public string Hour { get; set; }
}