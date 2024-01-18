using System.Linq.Expressions;
using AirQualityControlAPI.Application.Features.Sensors.Queries;
using AirQualityControlAPI.Application.Features.Sensors.Queries.GetSensorValuesBydatesAndVariables;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.Sensors.Queries;
using AutoMapper;
using Moq;

namespace AirQualityControlUnitTest.Sensors;
[TestClass]
public class GetSensorByDateAndVariableQueryHandlerTest
{
    private readonly ISensorQueryRepository _sensorQueryRepository;
    private readonly IMapper _mapper;
    private readonly GetSensorValuesByDatesAndVariablesQueryHandler _getSensorValuesByDatesAndVariablesQueryHandler;

    public GetSensorByDateAndVariableQueryHandlerTest()
    {
        _sensorQueryRepository = Mock.Of<ISensorQueryRepository>(MockBehavior.Strict);
        _mapper = Mock.Of<IMapper>(MockBehavior.Strict);
        _getSensorValuesByDatesAndVariablesQueryHandler =
            new GetSensorValuesByDatesAndVariablesQueryHandler(_sensorQueryRepository, _mapper);
    }

    [TestMethod]
    public async Task GivenSensorRequest_ReturnSensorList()
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
        var queryCommand = new GetSensorValuesByDatesAndVariablesQuery(DateTime.Now.ToString(),DateTime.Now.ToString(),It.IsAny<int>()) { };
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
            await _getSensorValuesByDatesAndVariablesQueryHandler.Handle(queryCommand, CancellationToken.None);
        Assert.IsNotNull(response);
    }
}