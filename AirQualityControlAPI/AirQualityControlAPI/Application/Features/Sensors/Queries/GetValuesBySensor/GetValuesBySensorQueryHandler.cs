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
            var dateFrom = new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day, 0, 0, 1,TimeSpan.Zero).ToString("dd-MM-yyyyTHH:mm:ss");
            var dateTo = new DateTimeOffset(DateTimeOffset.Now.Year, DateTimeOffset.Now.Month, DateTimeOffset.Now.Day, 23, 59, 59,TimeSpan.Zero).ToString("dd-MM-yyyyTHH:mm:ss");
            var algo = DateTimeOffset.ParseExact(dateFrom,  "dd-MM-yyyyTHH:mm:ss",CultureInfo.InvariantCulture);

            var algoTo =  DateTimeOffset.ParseExact(dateTo,  "dd-MM-yyyyTHH:mm:ss",CultureInfo.InvariantCulture);
            var pm10Values = new List<double>();
            var co2Values = new List<double>();
            var humidityValues = new List<double>();
            var pm25Values = new List<double>();
            var temperatureValues = new List<double>();
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
               NumberFormatInfo provider = new NumberFormatInfo();
               provider.NumberDecimalSeparator = ".";
               provider.NumberGroupSeparator = ",";
               switch (eachSensorValue.VariableId)
               {
                   case (int)VariableEnum.Pm10:
                       pm10Values.Add(Convert.ToDouble(eachSensorValue.Value,provider));
                       break;
                   case (int)VariableEnum.Co2:
                       co2Values.Add(Convert.ToDouble(eachSensorValue.Value,provider));
                       break;
                   case (int)VariableEnum.Humidity:
                       humidityValues.Add(Convert.ToDouble(eachSensorValue.Value,provider));
                       break;
                   case (int)VariableEnum.Pm25:
                       pm25Values.Add(Convert.ToDouble(eachSensorValue.Value,provider));
                       break;
                   case (int)VariableEnum.Temperature:
                       temperatureValues.Add(Convert.ToDouble(eachSensorValue.Value,provider));
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

    private List<SensorValuesDto> OrganiceData(List<double> pm10Values, List<double> pm25Values, List<double> co2Values,
        List<double> humidityValues, List<double> temperaturValues, List<SensorValuesDto> sensorValuesDtos)
    {
       sensorValuesDtos.Add(new SensorValuesDto()
       {
           
           Values = CalcularPromerio(pm10Values),
           Variable = VariableEnum.Pm10.ToString()

       });
       sensorValuesDtos.Add(new SensorValuesDto()
       {
           Values = CalcularPromerio(pm25Values),
           Variable = VariableEnum.Pm25.ToString()

       });
       sensorValuesDtos.Add(new SensorValuesDto()
       {
           Values = CalcularPromerio(co2Values),
           Variable = VariableEnum.Co2.ToString()

       });
       sensorValuesDtos.Add(new SensorValuesDto()
       {
           Values = CalcularPromerio(humidityValues),
           Variable = VariableEnum.Humidity.ToString()

       });
       sensorValuesDtos.Add(new SensorValuesDto()
       {
           Values = CalcularPromerio(temperaturValues),
           Variable = VariableEnum.Temperature.ToString()

       });
       return sensorValuesDtos;
    }

    private double CalcularPromerio(List<double> listValues)
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