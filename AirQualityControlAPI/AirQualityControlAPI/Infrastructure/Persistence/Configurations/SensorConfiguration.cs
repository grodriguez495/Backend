using AirQualityControlAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirQualityControlAPI.Infrastructure.Persistence.Configurations;

public class SensorConfiguration : IAirQualityControlSchemaEntityTypeConfiguration<Sensor>
{
    public void Configure(EntityTypeBuilder<Sensor> builder)
    {
        _ = builder.ToTable("sensor", "dbo")
            .HasKey(sensor => sensor.SensorId);
        
        _ = builder.Property(sensor => sensor.SensorId)
            .HasColumnName("sensor_id");
        _ = builder.Property(sensor => sensor.Timestamp)
            .HasColumnName("timestamp");
        _ = builder.Property(sensor => sensor.Latitud)
            .HasColumnName("latitud");
        _ = builder.Property(sensor => sensor.Longitud)
            .HasColumnName("longitud");
        _ = builder.Property(sensor => sensor.VariableId)
            .HasColumnName("variable_id");
        _ = builder.Property(sensor => sensor.Value)
            .HasColumnName("value");
    }
}