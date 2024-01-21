using AirQualityControlAPI.Domain.Repositories.Sensors.Queries;
using AutoMapper;
using MediatR;

namespace AirQualityControlAPI.Application.Features.Sensors.Queries.GetSensorIds;

public class GetSensorIdsQueryHandler : IRequestHandler<GetSensorIdsQuery, List<string>>
{
    private readonly ISensorQueryRepository _sensorQueryRepository;
    private readonly IMapper _mapper;

    public GetSensorIdsQueryHandler(ISensorQueryRepository sensorQueryRepository, IMapper mapper)
    {
        _sensorQueryRepository =
            sensorQueryRepository ?? throw new ArgumentNullException(nameof(sensorQueryRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    
    public async Task<List<string>> Handle(GetSensorIdsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var sensor = await _sensorQueryRepository.ListAsync(cancellationToken: cancellationToken);
            var result = sensor.Select(x => x.SensorId).Distinct();
            return result.ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new List<string>();
        }
    }
}