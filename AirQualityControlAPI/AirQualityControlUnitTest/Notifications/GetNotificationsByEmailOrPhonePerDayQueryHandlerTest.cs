using System.Linq.Expressions;
using AirQualityControlAPI.Application.Features.Notifications.Queries.GetNotificationsByEmailOrPhonePerDay;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.Alerts.Queries;
using Moq;

namespace AirQualityControlUnitTest.Notifications;

[TestClass]
public class GetNotificationsByEmailOrPhonePerDayQueryHandlerTest
{
    private readonly IAlertsQueryRepository _alertsQueryRepository;
    private readonly GetNotificationsByEmailOrPhonePerDayQueryHandler _getNotificationsByEmailOrPhonePerDayQueryHandler;

    public GetNotificationsByEmailOrPhonePerDayQueryHandlerTest()
    {
        _alertsQueryRepository = Mock.Of<IAlertsQueryRepository>(MockBehavior.Strict);
        _getNotificationsByEmailOrPhonePerDayQueryHandler =
            new GetNotificationsByEmailOrPhonePerDayQueryHandler(_alertsQueryRepository);
    }
    [TestMethod]
    public async Task GivenAPhoneAndEmail_ReturnNotificationList()
    {
        var notifications = new List<AlertNotification>()
        {
            new AlertNotification(1,DateTime.Now, "test","test",1,false),
            new AlertNotification(2,DateTime.Now, "test","test",1,false),
   
        };
        var command = new GetNotificationsByEmailOrPhonePerDayQuery("test", "test");
        
        Mock.Get(_alertsQueryRepository)
            .Setup(x => x.ListAsync(
                It.IsAny<Expression<Func<AlertNotification, bool>>>(),
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(notifications)
            .Verifiable();
        var response = await _getNotificationsByEmailOrPhonePerDayQueryHandler.Handle(command, CancellationToken.None);
        Assert.IsNotNull(response);
    }

}