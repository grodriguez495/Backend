namespace AirQualityControlAPI.Domain.Models;

public class Sensor : BaseEntity<string>
{
    public string SensorId { get; set; }
    public DateTimeOffset Timestamp { get; set; }
    public double Latitud { get; set; }
    public double Longitud { get; set; }
    public int VariableId { get; set; }
    public string Value { get; set; }


    public override string GetIdentity() => SensorId;
}