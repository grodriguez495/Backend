using System.Linq.Expressions;
using AirQualityControlAPI.Application.Features.Sensors.Queries.GetSensorGeographicInformations;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.Sensors.Queries;
using Moq;

namespace AirQualityControlUnitTest.Sensors;
[TestClass]
public class GetSensorGeographicInformationQueryHandlerTest
{
    private readonly ISensorQueryRepository _sensorQueryRepository;
    private readonly GetSensorGeographicInformationQueryHandler _geographicInformationQueryHandler;

    public GetSensorGeographicInformationQueryHandlerTest()
    {
        _sensorQueryRepository = Mock.Of<ISensorQueryRepository>(MockBehavior.Strict);
        _geographicInformationQueryHandler = new GetSensorGeographicInformationQueryHandler(_sensorQueryRepository);
    }

    [TestMethod]
    public async Task GivenRequest_ReturnSensorGeographicInformationDtoList()
    {
        var command = new GetSensorGeographicInformationQuery();

        var sensor = new Sensor()
        {
            SensorId = "test",
            Timestamp = DateTime.Now,
            Latitud = 2,
            Longitud = 2,
            VariableId = 1,
            Value = "1234"
        };
        Mock.Get(_sensorQueryRepository)
            .Setup(x => x.ListAsync(
                It.IsAny<Expression<Func<Sensor, bool>>>(),
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Sensor>() { sensor })
            .Verifiable();
        var response = await _geographicInformationQueryHandler.Handle(command, CancellationToken.None);
        Assert.IsNotNull(response);
}
}