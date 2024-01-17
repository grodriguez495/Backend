using AirQualityControlAPI.Domain.Enums;
using AirQualityControlAPI.Domain.Repositories.Sensors.Queries;
using AutoMapper;
using MediatR;

namespace AirQualityControlAPI.Application.Features.Sensors.Queries.GetSensorValuesBydatesAndVariables;

public class GetSensorValuesByDatesAndVariablesQueryHandler: IRequestHandler<GetSensorValuesByDatesAndVariablesQuery, List<SensorDto>>
{
    private readonly ISensorQueryRepository _sensorQueryRepository;
    private readonly IMapper _mapper;

    public GetSensorValuesByDatesAndVariablesQueryHandler(ISensorQueryRepository sensorQueryRepository,IMapper mapper)
    {
        _sensorQueryRepository = sensorQueryRepository ?? throw new ArgumentNullException(nameof(sensorQueryRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<List<SensorDto>> Handle(GetSensorValuesByDatesAndVariablesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            DateTime dateFrom = DateTime.Parse(request.DateFrom);
            DateTime dateTo = DateTime.Parse(request.DateTo);
            var entities = await _sensorQueryRepository.ListAsync(x =>
                x.VariableId == request.VariableId &&
                x.Timestamp.Date >= dateFrom.Date &&
                x.Timestamp.Date <= dateTo.Date,cancellationToken: cancellationToken);
            var result = _mapper.Map<List<SensorDto>>(entities);
            foreach (var eachResult in result)
            {
                var variableName = Enum.GetName(typeof(VariableEnum), eachResult.VariableId);
                eachResult.VariableName = variableName;
            }
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new List<SensorDto>();
        }
    }
}