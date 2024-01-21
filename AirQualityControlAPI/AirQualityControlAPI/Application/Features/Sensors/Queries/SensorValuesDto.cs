namespace AirQualityControlAPI.Application.Features.Sensors.Queries;

public class SensorValuesDto
{

    public List<string> Values { get; set; }
    public string Variable { get; set; }
}