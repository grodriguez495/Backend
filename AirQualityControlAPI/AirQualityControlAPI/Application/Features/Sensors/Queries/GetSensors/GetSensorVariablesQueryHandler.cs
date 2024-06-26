﻿using AirQualityControlAPI.Domain.Enums;
using AirQualityControlAPI.Domain.Repositories.Sensors.Queries;
using AutoMapper;
using MediatR;

namespace AirQualityControlAPI.Application.Features.Sensors.Queries.GetSensors;

public class GetSensorVariablesQueryHandler : IRequestHandler<GetSensorVariablesQuery,List<SensorDto>>
{
    private readonly ISensorQueryRepository _sensorQueryRepository;
    private readonly IMapper _mapper;
    public GetSensorVariablesQueryHandler(ISensorQueryRepository sensorQueryRepository,IMapper mapper )
    {
        _sensorQueryRepository = sensorQueryRepository ?? throw new ArgumentNullException(nameof(sensorQueryRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<List<SensorDto>> Handle(GetSensorVariablesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var entities = await _sensorQueryRepository.ListAsync(cancellationToken: cancellationToken);
            var result = _mapper.Map<List<SensorDto>>(entities);
            foreach (var eachResult in result)
            {
                var name = Enum.GetName(typeof(VariableEnum), eachResult.VariableId);
                eachResult.VariableName = name;
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