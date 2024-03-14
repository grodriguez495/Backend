namespace AirQualityControlAPI.Domain.Models;

public class AlertNotification : BaseEntity<int>
{
    public int AlertId { get; set; }
    public DateTimeOffset Timestamp { get; set; }

    public string Message { set; get; }
    public string Recipient { get; set; }
    public int AlertType { get; set; }
    public bool IsOpened { get; set; }
    public override int GetIdentity() => AlertId;

    public AlertNotification(int alertId,DateTimeOffset timestamp,string message, string recipient,int alertType,bool isOpened)
    {
        AlertId = alertId;
        Timestamp = timestamp;
        Message = message;
        Recipient = recipient;
        AlertType = alertType;
        IsOpened = isOpened;
    }
    public static AlertNotification NewAlerts(DateTimeOffset timestamp, string message, string recipient, int alertType, bool isOpened)
    {
        var entity = new AlertNotification(0, timestamp, message, recipient, alertType,isOpened);

        return entity;
    }
}