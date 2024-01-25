namespace AirQualityControlAPI.Application.Features.Sensors.Queries.GetSensorGeographicInformations;

public class SensorGeographicInformationDto
{
    public string SensorId { get; set; }
    public double Latitud { get; set; }
    public double Longitud { get; set; }
}