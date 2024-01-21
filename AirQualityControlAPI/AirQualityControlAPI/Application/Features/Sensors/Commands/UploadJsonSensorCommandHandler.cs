using System.Text;
using AirQualityControlAPI.Application.Interfaces;
using AirQualityControlAPI.Domain.Enums;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.Sensors.Commands;
using MediatR;

namespace AirQualityControlAPI.Application.Features.Sensors.Commands;

public class UploadJsonSensorCommandHandler : IRequestHandler<UploadJsonSensorCommand, bool>
{
    private readonly ISensorCommandRepository _sensorCommandRepository;
    private readonly ISendNotification _notification;


    public UploadJsonSensorCommandHandler(ISensorCommandRepository sensorCommandRepository, ISendNotification notification)
    {
        _sensorCommandRepository =
            sensorCommandRepository ?? throw new ArgumentNullException(nameof(sensorCommandRepository));
        _notification = notification ?? throw new ArgumentNullException(nameof(notification));
    }

    public async Task<bool> Handle(UploadJsonSensorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var sensorVariable = new Sensor();
            
            var inputFile = await ReadAsStringAsync(request.PostedFile);

            inputFile = CleanInput(inputFile);
            List<string> records = inputFile.Trim().Split('\r').ToList();
            sensorVariable.Timestamp = GetDateTime(records[1]);
            sensorVariable.SensorId = GetSensorId(records[0]);
            sensorVariable.Latitud = GetLatitud(records[3]);
            sensorVariable.Longitud = GetLongitud(records[4]);
            for (int i = 0; i <=6; i++)
            {
                records.RemoveAt(0);
            }
            foreach (var eachRecord in records)
            {
                var parametrosMediblesList = new List<string>()
                {
                    SensorProperties.Pm25,
                    SensorProperties.Pm10,
                    SensorProperties.Co2
                };
                 var sensorVariableValue = new VariableValue();
                var newRecord = eachRecord.Replace("\"", string.Empty).Trim().Split(":").ToList();

                if (parametrosMediblesList.Contains(newRecord[0]))
                {
                    var currentValue = float.Parse(newRecord[1].Trim().Replace(".", ","));
                    if (currentValue >=  SensorClasificationEnum.MinDañinaParaGruposSensibles &&
                        currentValue <= SensorClasificationEnum.MaxDañinaParaGruposSensibles)
                    {
                        sensorVariableValue.Value = currentValue.ToString();
                        sensorVariableValue.Clasificacion = "Dañina a la salud para grupos sensibles";
                    }

                    if (currentValue >= SensorClasificationEnum.MinDañinaALaSalud &&
                        currentValue <= SensorClasificationEnum.MaxDañinaALaSalud)
                    {
                        sensorVariableValue.Value = currentValue.ToString();
                        sensorVariableValue.Clasificacion = "Dañina a la salud";
                    }

                    if (currentValue >= SensorClasificationEnum.MinMuyDañinaALaSalud &&
                        currentValue <= SensorClasificationEnum.MaxMuyDañinaALaSalud)
                    {
                        sensorVariableValue.Value = currentValue.ToString();
                        sensorVariableValue.Clasificacion = "Muy dañina a la salud";
                    }

                    if (currentValue >= SensorClasificationEnum.MinPeligrosaALaSalud &&
                        currentValue <= SensorClasificationEnum.MaxPeligrosaALaSalud)
                    {
                        sensorVariableValue.Value = currentValue.ToString();
                        sensorVariableValue.Clasificacion = "Peligrosa";
                    }
                }

                switch (newRecord[0])
                {
                    case SensorProperties.Pm25:
                    {
                        sensorVariable.VariableId = (int)VariableEnum.Pm25;
                        sensorVariable.Value = newRecord[1].Trim();
                        sensorVariableValue.VariableName = SensorProperties.Pm25;
                        break;
                    }
                    case SensorProperties.Pm10:
                    {
                        sensorVariable.VariableId = (int)VariableEnum.Pm10;
                        sensorVariable.Value = newRecord[1].Trim();
                        sensorVariableValue.VariableName = SensorProperties.Pm10;
                        break;
                    }
                    case SensorProperties.Co2:
                    {
                        sensorVariable.VariableId = (int)VariableEnum.Co2;
                        sensorVariable.Value = newRecord[1].Trim();
                        sensorVariableValue.VariableName = SensorProperties.Co2;
                        break;
                    }
                    case SensorProperties.Humidity:
                    {
                        sensorVariable.VariableId = (int)VariableEnum.Humidity;
                        sensorVariable.Value = newRecord[1].Trim();
                        break;
                    }
                    case SensorProperties.Temperature:
                    {
                        sensorVariable.VariableId = (int)VariableEnum.Temperature;
                        sensorVariable.Value = newRecord[1].Trim();
                        break;
                    }
                }
                

                await _sensorCommandRepository.RegisterAsync(sensorVariable, cancellationToken);
                if (!string.IsNullOrWhiteSpace(sensorVariableValue.Value))
                {
                    await _notification.SendEmailNotificationAsync(sensorVariableValue,cancellationToken);
                    await _notification.SendSmsNotificationAsync(sensorVariableValue,cancellationToken);
                }
            }

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }

    }

    private double GetLongitud(string record)
    {
      var result= record.Replace("\"", string.Empty).Trim().Split(":").ToList();
      return double.Parse(result[1].Trim(), System.Globalization.CultureInfo.InvariantCulture);
    }

    private double GetLatitud(string record)
    {
        var result= record.Replace("\"", string.Empty).Trim().Split(":").ToList();
        return double.Parse(result[1].Trim(), System.Globalization.CultureInfo.InvariantCulture);
    }

    private string GetSensorId(string record)
    {
        var splitRecord = record.Replace("\"", string.Empty).Trim().Split(':').ToList();
        return splitRecord[1].Trim();
    }

    private DateTime GetDateTime(string record)
    {
        string splitValue = ": \"";
        var splitRecord = record.Trim().Split(splitValue).ToList();
        return DateTime.Parse(splitRecord[1][..^1]);
    }

    private string CleanInput(string inputFile)
    {
        inputFile = inputFile.Replace("{", string.Empty);
        inputFile = inputFile.Replace("}", string.Empty);
        inputFile = inputFile.Replace(",", string.Empty);
        return inputFile;
    }

    private async Task<string> ReadAsStringAsync(Stream stream)
    {
        var result = new StringBuilder();
        using (var reader = new StreamReader(stream))
        {
            while (reader.Peek() >= 0)
                result.AppendLine(await reader.ReadLineAsync());
        }

        return result.ToString();
    }
}