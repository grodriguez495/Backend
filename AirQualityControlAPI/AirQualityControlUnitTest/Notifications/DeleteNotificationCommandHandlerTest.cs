using AirQualityControlAPI.Application.Features.Notifications.Commands.Delete;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.Alerts.Commands;
using AirQualityControlAPI.Domain.Repositories.Alerts.Queries;
using Moq;

namespace AirQualityControlUnitTest.Notifications;
[TestClass]
public class DeleteNotificationCommandHandlerTest
{
    private readonly IAlertsQueryRepository _alertsQueryRepository;
    private readonly IAlertsCommandRepository _alertsCommandRepository;
    private readonly DeleteNotificationCommandHandler _deleteNotificationCommandHandler;

    public DeleteNotificationCommandHandlerTest()
    {
        _alertsQueryRepository = Mock.Of<IAlertsQueryRepository>(MockBehavior.Strict);
        _alertsCommandRepository = Mock.Of<IAlertsCommandRepository>(MockBehavior.Strict);
        _deleteNotificationCommandHandler =
            new DeleteNotificationCommandHandler(_alertsQueryRepository, _alertsCommandRepository);
    }

    [TestMethod]
    public async Task GivenExistingAlert_DeleteAlert()
    {
        var alertNotification = new AlertNotification(1, DateTime.Now, "test", "test", 1, false);
        var command = new DeleteNotificationCommand()
        {
            Id = 1
        };
        Mock.Get(_alertsQueryRepository)
            .Setup(x => x.FindAsync(
                It.IsAny<int>(),
                    It.IsAny<bool>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(alertNotification)
            .Verifiable();
        Mock.Get(_alertsCommandRepository)
            .Setup(x => x.EditAsync(
                It.IsAny<AlertNotification>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true)
            .Verifiable();
        var response = await _deleteNotificationCommandHandler.Handle(command, CancellationToken.None);
        Assert.IsTrue(response);
    }
}