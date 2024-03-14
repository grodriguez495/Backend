namespace AirQualityControlAPI.Application.Features.Sensors.Queries;

public class SensorDto
{
    public string SensorId { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    public double Latitud { get; set; }
    public double Longitud { get; set; }
    public int VariableId { get; set; }
    public string Value { get; set; }
    public string VariableName { get; set; }
}