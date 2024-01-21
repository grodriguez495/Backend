namespace AirQualityControlAPI.Application.Features.Notifications;

public class NotificationDto
{
    public int AlertId { get; set; }
    public string Timestamp { get; set; }
    public string Message { set; get; }
    public string Recipient { get; set; }
    public int AlertType { get; set; }
    public string AlertTypeName { get; set; }
    public bool IsOpened { get; set; }

}