using System.Text.Json.Serialization;
using MediatR;

namespace AirQualityControlAPI.Application.Features.Sensors.Commands;

public class UploadJsonSensorCommand: IRequest<bool>
{
    [JsonConstructor]
    public UploadJsonSensorCommand()
    {
    }

    public Stream PostedFile { get; set; }
}