using System.Linq.Expressions;
using AirQualityControlAPI.Application.Features.Notifications.Queries.GetActiveNotifications;
using AirQualityControlAPI.Application.Features.Notifications.Queries.GetNotificationsByEmailOrPhone;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.Alerts.Queries;
using Moq;

namespace AirQualityControlUnitTest.Notifications;
[TestClass]
public class GetNotificationByEmailOrPhoneQueryHandlerTest
{
    private readonly IAlertsQueryRepository _alertsQueryRepository;
    public readonly GetNotificationByEmailOrPhoneQueryHandler _getNotificationByEmailOrPhoneQueryHandler;

    public GetNotificationByEmailOrPhoneQueryHandlerTest()
    {
        _alertsQueryRepository = Mock.Of<IAlertsQueryRepository>(MockBehavior.Strict);
        _getNotificationByEmailOrPhoneQueryHandler =
            new GetNotificationByEmailOrPhoneQueryHandler(_alertsQueryRepository);
        
    }
    [TestMethod]
    public async Task GivenAPhoneAndEmail_ReturnNotificationList()
    {
        var notifications = new List<AlertNotification>()
        {
            new AlertNotification(1,DateTime.Now, "test","test",1,false),
            new AlertNotification(2,DateTime.Now, "test","test",1,false),
   
        };
        var command = new GetNotificationByEmailOrPhoneQuery("test", "test");
        
        Mock.Get(_alertsQueryRepository)
            .Setup(x => x.ListAsync(
                It.IsAny<Expression<Func<AlertNotification, bool>>>(),
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(notifications)
            .Verifiable();
        var response = await _getNotificationByEmailOrPhoneQueryHandler.Handle(command, CancellationToken.None);
        Assert.IsNotNull(response);
    }
}