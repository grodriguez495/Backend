using System.Globalization;
using AirQualityControlAPI.Domain.Repositories.Sensors.Queries;
using MediatR;

namespace AirQualityControlAPI.Application.Features.Sensors.Queries.GetSensorValuesByDateAndVariableAndSensor;

public class GetSensorValuesByDateAndVariableAndSensorQueryHandler : IRequestHandler<GetSensorValuesByDateAndVariableAndSensorQuery,List<SensorValuesDto>>
{
    private readonly ISensorQueryRepository _sensorQueryRepository;
    
    public GetSensorValuesByDateAndVariableAndSensorQueryHandler(ISensorQueryRepository sensorQueryRepository)
    {
        _sensorQueryRepository = sensorQueryRepository ?? throw new ArgumentNullException(nameof(sensorQueryRepository));
    }

    public async  Task<List<SensorValuesDto>> Handle(GetSensorValuesByDateAndVariableAndSensorQuery request, CancellationToken cancellationToken)
    {
        var finalList = new List<SensorValuesDto>();
        var initialFrom = DateTime.Parse(request.DateFrom);
        var initialTo = DateTime.Parse(request.DateTo);
        var dateFrom = new DateTime(initialFrom.Year, initialFrom.Month, initialFrom.Day, 0, 0, 1).ToString("dd-MM-yyyyTHH:mm:ss");
        var dateTo = new DateTime(initialTo.Year, initialTo.Month, initialTo.Day, 23, 59, 59).ToString("dd-MM-yyyyTHH:mm:ss");
        var dateFromFinal = DateTime.ParseExact(dateFrom,  "dd-MM-yyyyTHH:mm:ss",CultureInfo.InvariantCulture);
        var dateToFinal =  DateTime.ParseExact(dateTo,  "dd-MM-yyyyTHH:mm:ss",CultureInfo.InvariantCulture);
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
            return new List<SensorValuesDto>();

        var groupByDate = sensorValuesList.GroupBy(x => x.Timestamp.Date);
        foreach (var eachSensorValue in groupByDate)
        {
            var currentStringValues = eachSensorValue.Select(x => x.Value).ToList();
            double promedio = GenerarPromedio(currentStringValues.Select(x =>Convert.ToDouble(x,provider)).ToList());
            var newDto = new SensorValuesDto() { Values = promedio,Variable = eachSensorValue.Key.ToString("dd-MM-yyyy")};
            finalList.Add(newDto);
        }

        return finalList;
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