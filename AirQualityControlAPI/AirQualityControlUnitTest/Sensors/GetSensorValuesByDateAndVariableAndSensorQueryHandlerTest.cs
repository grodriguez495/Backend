using System.Linq.Expressions;
using AirQualityControlAPI.Application.Features.Sensors.Queries.GetSensorValuesByDateAndVariableAndSensor;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.Sensors.Queries;
using Moq;

namespace AirQualityControlUnitTest.Sensors;
[TestClass]
public class GetSensorValuesByDateAndVariableAndSensorQueryHandlerTest
{
    private readonly ISensorQueryRepository _sensorQueryRepository;

    private readonly GetSensorValuesByDateAndVariableAndSensorQueryHandler
        _getSensorValuesByDateAndVariableAndSensorQueryHandler;

    public GetSensorValuesByDateAndVariableAndSensorQueryHandlerTest()
    {
        _sensorQueryRepository = Mock.Of<ISensorQueryRepository>(MockBehavior.Strict);
        _getSensorValuesByDateAndVariableAndSensorQueryHandler =
            new GetSensorValuesByDateAndVariableAndSensorQueryHandler(_sensorQueryRepository);
        
    }

    public async Task GivenRequest_ReturnSensorList()
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
        var command = new GetSensorValuesByDateAndVariableAndSensorQuery("test", "test", 1, "test");
        Mock.Get(_sensorQueryRepository)
            .Setup(x => x.ListAsync(
                It.IsAny<Expression<Func<Sensor, bool>>>(),
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Sensor>() { sensor })
            .Verifiable();
        var response =
            await _getSensorValuesByDateAndVariableAndSensorQueryHandler.Handle(command, CancellationToken.None);
        Assert.IsNotNull(response);
    }
}