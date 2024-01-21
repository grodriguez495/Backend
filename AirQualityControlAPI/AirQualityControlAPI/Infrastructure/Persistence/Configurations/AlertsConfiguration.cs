using AirQualityControlAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirQualityControlAPI.Infrastructure.Persistence.Configurations;

public class AlertsConfiguration :IAirQualityControlSchemaEntityTypeConfiguration<AlertNotification>
{
    public void Configure(EntityTypeBuilder<AlertNotification> builder)
    {
        _ = builder.ToTable("alert_notification", "dbo")
            .HasKey(alert => alert.AlertId);
        _ = builder
            .Property(alert => alert.AlertId)
            .HasColumnName("alert_id");
        _ = builder
            .Property(alert => alert.Recipient)
            .HasColumnName("recipient")
            .IsRequired();
        _ = builder
            .Property(alert => alert.AlertType)
            .HasColumnName("alert_type")
            .IsRequired();
        _ = builder
            .Property(alert => alert.Message)
            .HasColumnName("message")
            .IsRequired();
        _ = builder
            .Property(alert => alert.Timestamp)
            .HasColumnName("timestamp")
            .IsRequired();
    }
}