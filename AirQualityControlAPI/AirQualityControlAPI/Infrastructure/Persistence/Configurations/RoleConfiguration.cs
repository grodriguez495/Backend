using AirQualityControlAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirQualityControlAPI.Infrastructure.Persistence.Configurations
{
    public class RoleConfiguration : IAirQualityControlSchemaEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            _ = builder.ToTable("role", "dbo")
                  .HasKey(role => role.RoleId);

            _ = builder
                .Property(role => role.RoleId)
                .HasColumnName("role_id");
            _ = builder
                .Property(role => role.Name)
                .HasColumnName("name")
                .IsRequired();
        }
    }
}
