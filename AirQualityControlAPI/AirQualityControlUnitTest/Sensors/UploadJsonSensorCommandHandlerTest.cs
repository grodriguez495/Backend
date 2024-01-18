using System.Text;
using AirQualityControlAPI.Application.Features.Sensors.Commands;
using AirQualityControlAPI.Application.Interfaces;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.Sensors.Commands;
using Moq;

namespace AirQualityControlUnitTest.Sensors;

[TestClass]
public class UploadJsonSensorCommandHandlerTest
{
    private readonly ISensorCommandRepository _sensorCommandRepository;
    private readonly ISendNotification _notification;
    private readonly UploadJsonSensorCommandHandler _uploadJsonSensorCommandHandler;


    public UploadJsonSensorCommandHandlerTest()
    {
        _sensorCommandRepository = Mock.Of<ISensorCommandRepository>(MockBehavior.Strict);
        _notification = Mock.Of<ISendNotification>(MockBehavior.Strict);
        _uploadJsonSensorCommandHandler = new UploadJsonSensorCommandHandler(_sensorCommandRepository, _notification);
    }

    [TestMethod]
    public async Task GivenSensorUploadRequest_ReturnSensorList()
    {
        var json =
            $"{{\n  \"sensor_id\": \"AQS12\",\n  \"timestamp\": \"2022-01-05T12:00:00Z\",\n  \"location\": {{\n    \"latitude\": 38.7749,\n    \"longitude\": -129.4194\n  }},\n  \"parameters\": {{\n    \"PM2.5\": 20.0,\n    \"PM10\": 41.7,\n    \"CO2\": 25.03,\n    \"temperature\": 15.5,\n    \"humidity\":\u00a07.4\n\u00a0\u00a0}}\n}}";
        var command = new UploadJsonSensorCommand()
        {
            PostedFile = new MemoryStream(Encoding.UTF8.GetBytes(json))
        };
        Mock.Get(_sensorCommandRepository)
            .Setup(x => x.RegisterAsync(
                It.IsAny<Sensor>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true)
            .Verifiable();
        Mock.Get(_notification)
            .Setup(x => x.SendEmailNotificationAsync(
                It.IsAny<VariableValue>(),
                It.IsAny<CancellationToken>()))
            .Verifiable();
        Mock.Get(_notification)
            .Setup(x => x.SendSmsNotificationAsync(
                It.IsAny<VariableValue>(),
                It.IsAny<CancellationToken>()))
            .Verifiable();
        var response = await _uploadJsonSensorCommandHandler.Handle(command, CancellationToken.None);
        Assert.IsTrue(response);
    }
}