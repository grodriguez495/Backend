using System.Globalization;
using AirQualityControlAPI.Domain.Enums;
using AirQualityControlAPI.Domain.Repositories.Sensors.Queries;
using MediatR;

namespace AirQualityControlAPI.Application.Features.Sensors.Queries.GetValuesBySensor;

public class GetValuesBySensorQueryHandler : IRequestHandler<GetValuesBySensorQuery,List<SensorValuesDto>>
{
    private readonly ISensorQueryRepository _sensorQueryRepository;
    
    public GetValuesBySensorQueryHandler(ISensorQueryRepository sensorQueryRepository)
    {
        _sensorQueryRepository = sensorQueryRepository ?? throw new ArgumentNullException(nameof(sensorQueryRepository));
    }

    public async Task<List<SensorValuesDto>> Handle(GetValuesBySensorQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var dateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 1).ToString("dd-MM-yyyyTHH:mm:ss");
            var dateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59).ToString("dd-MM-yyyyTHH:mm:ss");
            var algo = DateTime.ParseExact(dateFrom,  "dd-MM-yyyyTHH:mm:ss",CultureInfo.InvariantCulture);
         
            var algoTo =  DateTime.ParseExact(dateTo,  "dd-MM-yyyyTHH:mm:ss",CultureInfo.InvariantCulture);
            var pm10Values = new List<string>();
            var co2Values = new List<string>();
            var humidityValues = new List<string>();
            var pm25Values = new List<string>();
            var temperatureValues = new List<string>();
            var finalList = new List<SensorValuesDto>();
            
            var sensorValues =
                await _sensorQueryRepository.ListAsync(x => 
                    x.SensorId.Equals(request.Sensor)
                    , false,
                    cancellationToken);
           var sensorValuesList = sensorValues.Where(x=> x.Timestamp >= algo &&
                                                                  x.Timestamp <= algoTo ).ToList();
           var groupBy = sensorValuesList.GroupBy(x => x.VariableId);
           foreach (var eachSensorValue in sensorValuesList)
           {
               switch (eachSensorValue.VariableId)
               {
                   case (int)VariableEnum.Pm10:
                       pm10Values.Add(eachSensorValue.Value);
                       break;
                   case (int)VariableEnum.Co2:
                       co2Values.Add(eachSensorValue.Value);
                       break;
                   case (int)VariableEnum.Humidity:
                       humidityValues.Add(eachSensorValue.Value);
                       break;
                   case (int)VariableEnum.Pm25:
                       pm25Values.Add(eachSensorValue.Value);
                       break;
                   case (int)VariableEnum.Temperature:
                       temperatureValues.Add(eachSensorValue.Value);
                       break;
               }
           }

           var result  = OrganiceData(pm10Values, pm25Values, co2Values, humidityValues, temperatureValues,finalList);
           return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new List<SensorValuesDto>();
        }
    }

    private List<SensorValuesDto> OrganiceData(List<string> pm10Values, List<string> pm25Values, List<string> co2Values,
        List<string> humidityValues, List<string> temperaturValues, List<SensorValuesDto> sensorValuesDtos)
    {
       sensorValuesDtos.Add(new SensorValuesDto()
       {
           Values = pm10Values,
           Variable = VariableEnum.Pm10.ToString()
               
       });
       sensorValuesDtos.Add(new SensorValuesDto()
       {
           Values = pm25Values,
           Variable = VariableEnum.Pm25.ToString()
               
       });
       sensorValuesDtos.Add(new SensorValuesDto()
       {
           Values = co2Values,
           Variable = VariableEnum.Co2.ToString()
               
       });
       sensorValuesDtos.Add(new SensorValuesDto()
       {
           Values = humidityValues,
           Variable = VariableEnum.Humidity.ToString()
               
       });
       sensorValuesDtos.Add(new SensorValuesDto()
       {
           Values = temperaturValues,
           Variable = VariableEnum.Temperature.ToString()
               
       });
       return sensorValuesDtos;
    }
}