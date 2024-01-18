using System.Linq.Expressions;
using AirQualityControlAPI.Application.Features.Sensors.Queries;
using AirQualityControlAPI.Application.Features.Sensors.Queries.GetSensors;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.Sensors.Queries;
using AutoMapper;
using Moq;

namespace AirQualityControlUnitTest.Sensors;

[TestClass]
public class GetSensorVariablesQueryHandlerTest
{
    private readonly ISensorQueryRepository _sensorQueryRepository;
    private readonly IMapper _mapper;
    private readonly GetSensorVariablesQueryHandler _getSensorVariablesQueryHandler;

    public GetSensorVariablesQueryHandlerTest()
    {
        _sensorQueryRepository = Mock.Of<ISensorQueryRepository>(MockBehavior.Strict);
        _mapper = Mock.Of<IMapper>(MockBehavior.Strict);
        _getSensorVariablesQueryHandler = new GetSensorVariablesQueryHandler(_sensorQueryRepository, _mapper);
    }

    [TestMethod]
    public async Task GivensensorRequest_ReturnSensorList()
    {
        var sensor = new Sensor()
        {
            SensorId = "test",
            Timestamp = DateTime.Now,
            Latitud = 2,
            Longitud = 2,
            VariableId = 1,
            Value = "1234"
        };
        var sensorDto = new SensorDto();
        Mock.Get(_sensorQueryRepository)
            .Setup(x => x.ListAsync(
                It.IsAny<Expression<Func<Sensor, bool>>>(),
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Sensor>() { sensor })
            .Verifiable();
        Mock.Get(_mapper)
            .Setup(x => x.Map<List<SensorDto>>(new List<Sensor>() { sensor }))
            .Returns(new List<SensorDto>() { sensorDto })
            .Verifiable();
        var response =
            await _getSensorVariablesQueryHandler.Handle(new GetSensorVariablesQuery(), CancellationToken.None);
        Assert.IsNotNull(response);
    }
}