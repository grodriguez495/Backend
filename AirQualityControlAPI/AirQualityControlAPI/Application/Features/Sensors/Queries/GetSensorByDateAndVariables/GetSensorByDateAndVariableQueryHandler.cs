using AirQualityControlAPI.Application.Features.Roles;
using AirQualityControlAPI.Domain.Enums;
using AirQualityControlAPI.Domain.Repositories.Sensors.Queries;
using AutoMapper;
using MediatR;

namespace AirQualityControlAPI.Application.Features.Sensors.Queries.GetSensorByDateAndVariables;

public class GetSensorByDateAndVariableQueryHandler : IRequestHandler<GetSensorByDateAndVariableQuery,List<SensorDto>>
{
    private readonly ISensorQueryRepository _sensorQueryRepository;
    private readonly IMapper _mapper;
    
    public GetSensorByDateAndVariableQueryHandler(ISensorQueryRepository sensorQueryRepository,IMapper mapper)
    {
        _sensorQueryRepository = sensorQueryRepository ?? throw new ArgumentNullException(nameof(sensorQueryRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<List<SensorDto>> Handle(GetSensorByDateAndVariableQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var entities = await _sensorQueryRepository.ListAsync(x =>
                x.VariableId == request.VariableId,cancellationToken: cancellationToken);
            var result = _mapper.Map<List<SensorDto>>(entities);
            foreach (var eachResult in result)
            {
                var algo = Enum.GetName(typeof(VariableEnum), eachResult.VariableId);
                eachResult.VariableName = algo;
            }
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return new List<SensorDto>();
        }
    }
    
}