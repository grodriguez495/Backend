using AirQualityControlAPI.Domain.Repositories.Sensors.Queries;
using MediatR;

namespace AirQualityControlAPI.Application.Features.Sensors.Queries.GetSensorGeographicInformations;

public class GetSensorGeographicInformationQueryHandler: IRequestHandler<GetSensorGeographicInformationQuery, List<SensorGeographicInformationDto>>
{
    private readonly ISensorQueryRepository _sensorQueryRepository;

    public GetSensorGeographicInformationQueryHandler(ISensorQueryRepository sensorQueryRepository)
    {
        _sensorQueryRepository =
            sensorQueryRepository ?? throw new ArgumentNullException(nameof(sensorQueryRepository));
    }


    public async Task<List<SensorGeographicInformationDto>> Handle(GetSensorGeographicInformationQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = new List<SensorGeographicInformationDto>();
            var sensorResponse = await _sensorQueryRepository.ListAsync(cancellationToken: cancellationToken);
            var sensorList = sensorResponse.GroupBy(x=>x.SensorId).ToList().Distinct();
            foreach (var eachSensor in sensorList)
            {
                var sensor = new SensorGeographicInformationDto
                {
                    SensorId = eachSensor.First().SensorId,
                    Latitud = eachSensor.First().Latitud,
                    Longitud = eachSensor.First().Longitud
                };
                result.Add(sensor);
            }

            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new List<SensorGeographicInformationDto>();
        }
    }
}