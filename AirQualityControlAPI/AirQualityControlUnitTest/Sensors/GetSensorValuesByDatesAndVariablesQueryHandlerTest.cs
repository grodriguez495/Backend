using System.Linq.Expressions;
using AirQualityControlAPI.Application.Features.Sensors.Queries;
using AirQualityControlAPI.Application.Features.Sensors.Queries.GetSensorByDateAndVariables;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.Sensors.Queries;
using AutoMapper;
using Moq;

namespace AirQualityControlUnitTest.Sensors;
[TestClass]
public class GetSensorValuesByDatesAndVariablesQueryHandlerTest
{
    private readonly ISensorQueryRepository _sensorQueryRepository;
    private readonly IMapper _mapper;
    private readonly GetSensorByDateAndVariableQueryHandler _getSensorByDateAndVariableQueryHandler;

    public GetSensorValuesByDatesAndVariablesQueryHandlerTest()
    {
        _sensorQueryRepository = Mock.Of<ISensorQueryRepository>(MockBehavior.Strict);
        _mapper = Mock.Of<IMapper>(MockBehavior.Strict);
        _getSensorByDateAndVariableQueryHandler =
            new (_sensorQueryRepository, _mapper);

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
        var queryCommand =
            new GetSensorByDateAndVariableQuery(
                It.IsAny<int>(), It.IsAny<int>()) { };
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
            await _getSensorByDateAndVariableQueryHandler.Handle(queryCommand, CancellationToken.None);
        Assert.IsNotNull(response);
    }
}