using System;
using System.Text;
using System.Text.RegularExpressions;
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
    private readonly ILogger<UploadJsonSensorCommandHandler> _logger;


    public UploadJsonSensorCommandHandler(ISensorCommandRepository sensorCommandRepository,
        ISendNotification notification, ILogger<UploadJsonSensorCommandHandler> logger)
    {
        _sensorCommandRepository =
            sensorCommandRepository ?? throw new ArgumentNullException(nameof(sensorCommandRepository));
        _notification = notification ?? throw new ArgumentNullException(nameof(notification));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<bool> Handle(UploadJsonSensorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Ingreso a guardar información de sensores");
            var sensorVariable = new Sensor();

            var inputFile = await ReadAsStringAsync(request.PostedFile);

            inputFile = CleanInput(inputFile);
            _logger.LogInformation("Ingreso a guardar información de sensores");
            _logger.LogInformation($"ingreso esta informacion: {inputFile}");
            List<string> records = SplitJson(inputFile);
            _logger.LogInformation($"lista de records: {records.Count}");
            sensorVariable.Timestamp = GetDateTime(records[2]);
            _logger.LogInformation($"Fecha tomada del sensor: {sensorVariable.Timestamp}");
            sensorVariable.SensorId = GetSensorId(records[1]);
            _logger.LogInformation($"Id tomada del sensor: {sensorVariable.SensorId}");
            sensorVariable.Latitud = GetLatitud(records[4]);
            _logger.LogInformation($"Latitud tomada del sensor: {sensorVariable.Latitud}");
            sensorVariable.Longitud = GetLongitud(records[5]);
            _logger.LogInformation($"Longitud tomada del sensor: {sensorVariable.Longitud}");
            for (int i = 0; i <= 7; i++)
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
                List<VariableValue> sensorVariableList = new List<VariableValue>();
                var sensorVariableValue = new VariableValue();
                var newRecord = eachRecord.Replace("\"", string.Empty).Trim().Split(":").ToList();

                if (parametrosMediblesList.Contains(newRecord[0]))
                {
                    var currentValue = float.Parse(newRecord[1].Trim().Replace(".", ","));
                    if (currentValue >= SensorClasificationEnum.MinDañinaParaGruposSensibles &&
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

                    if (currentValue >= SensorClasificationEnum.MinPeligrosaALaSalud )
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
                    _logger.LogInformation($"Envio de notificaciones");
                    await _notification.SendEmailNotificationAsync(sensorVariableValue, cancellationToken);
                    await _notification.SendSmsNotificationAsync(sensorVariableValue, cancellationToken);
                }
            }
            _logger.LogInformation($"Guardo correctamente la información");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError($"se presento el sieguiente error: {e.Message}");
            Console.WriteLine(e);
            return false;
        }

    }

    private List<string> SplitJson(string inputFile)
    {
        inputFile = Regex.Replace(inputFile, @"[^\S\r\n]+", "");
        // Suponiendo que 'json' es tu JSON completo
        var lines = new List<string>();
        var currentLine = new StringBuilder();
        bool insideString = false;
        int level = 0;

        foreach (char c in inputFile)
        {
            if (c == '{' || c == '[')
            {
                level++;
            }
            else if (c == '}' || c == ']')
            {
                level--;
            }

            if (c == '"')
            {
                insideString = !insideString;
            }

            if (c == '\n' && level == 0 && !insideString)
            {
                lines.Add(currentLine.ToString());
                currentLine.Clear();
            }
            else
            {
                currentLine.Append(c);
            }
        }

        if (currentLine.Length > 0)
        {
            lines.Add(currentLine.ToString());
        }
        return lines;
    }

    private double GetLongitud(string record)
    {
        var result = record.Replace("\"", string.Empty).Trim().Split(":").ToList();
        return double.Parse(result[1].Trim(), System.Globalization.CultureInfo.InvariantCulture);
    }

    private double GetLatitud(string record)
    {
        var result = record.Replace("\"", string.Empty).Trim().Split(":").ToList();
        return double.Parse(result[1].Trim(), System.Globalization.CultureInfo.InvariantCulture);
    }

    private string GetSensorId(string record)
    {
        var splitRecord = record.Replace("\"", string.Empty).Trim().Split(':').ToList();
        return splitRecord[1].Trim();
    }

    private DateTimeOffset GetDateTime(string record)
    {
         record = record.Replace("\"", "");
        string splitValue = "p:";
        var splitRecord = record.Trim().Split(splitValue).ToList();
        return DateTimeOffset.Parse(splitRecord[1][..^1]);
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