namespace AirQualityControlAPI.Domain.Models;

public partial class AlertType : BaseEntity<int>
{
    public int Id { get; set; }
    public string AlertName { get; set; }

    public virtual AlertNotification AlertNotification { get; set; }

    public override int GetIdentity() => Id;
}


