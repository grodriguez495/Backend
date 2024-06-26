﻿using System.Globalization;
using AirQualityControlAPI.Domain.Enums;
using AirQualityControlAPI.Domain.Repositories.Sensors.Queries;
using MediatR;

namespace AirQualityControlAPI.Application.Features.Sensors.Queries.GetValuesBySensorAndVariablePerDay;

public class
    GetValuesByVariableAndSensorQueryHandler : IRequestHandler<GetValuesByVariableAndSensorQuery,
        List<ValuesBySensorAndVariableDto>>
{
    private readonly ISensorQueryRepository _sensorQueryRepository;
 

    public GetValuesByVariableAndSensorQueryHandler(ISensorQueryRepository sensorQueryRepository)
    {
        _sensorQueryRepository =
            sensorQueryRepository ?? throw new ArgumentNullException(nameof(sensorQueryRepository));
    }

    public async Task<List<ValuesBySensorAndVariableDto>> Handle(GetValuesByVariableAndSensorQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var finalList = new List<ValuesBySensorAndVariableDto>();
            var initialFrom = DateTimeOffset.Now;
            var initialTo = DateTimeOffset.Now;
            var dateFrom = new DateTimeOffset(initialFrom.Year, initialFrom.Month, initialFrom.Day, 0, 0, 1,TimeSpan.Zero).ToString("dd-MM-yyyyTHH:mm:ss");
            var dateTo = new DateTimeOffset(initialTo.Year, initialTo.Month, initialTo.Day, 23, 59, 59,TimeSpan.Zero).ToString("dd-MM-yyyyTHH:mm:ss");
            var dateFromFinal = DateTimeOffset.ParseExact(dateFrom,  "dd-MM-yyyyTHH:mm:ss",CultureInfo.InvariantCulture);
            var dateToFinal =  DateTimeOffset.ParseExact(dateTo,  "dd-MM-yyyyTHH:mm:ss",CultureInfo.InvariantCulture);
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            provider.NumberGroupSeparator = ",";
            
            var sensorValues =
                await _sensorQueryRepository.ListAsync(x => 
                        x.SensorId.Equals(request.Sensor) &&
                        x.VariableId == request.VariableId
                    , false,
                    cancellationToken);
            var sensorValuesList = sensorValues.Where(x=> x.Timestamp >= dateFromFinal &&
                                                          x.Timestamp <= dateToFinal ).ToList();

            if (!sensorValuesList.Any())
                return new List<ValuesBySensorAndVariableDto>();

            var groupByDate = sensorValuesList.GroupBy(x => x.Timestamp.Hour);
            foreach (var eachSensorValue in groupByDate)
            {
                var currentStringValues = eachSensorValue.Select(x => x.Value).ToList();
                double promedio = GenerarPromedio(currentStringValues.Select(x =>Convert.ToDouble(x,provider)).ToList());
                var newDto = new ValuesBySensorAndVariableDto() { Value = promedio,Hour = TimeSpan.FromHours(eachSensorValue.Key).ToString(@"hh\:mm")};
                finalList.Add(newDto);
            }

            return finalList.OrderBy(x=> x.Hour).ToList();
   
       
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private double GenerarPromedio(List<double> listValues)
    {
        if (listValues.Count == 0)
            return 0;
        double suma = 0;
        foreach (double numero in listValues)
        {
            suma += numero;
        }

        return (suma / listValues.Count);
    }
}