using AirQualityControlAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirQualityControlAPI.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IAirQualityControlSchemaEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        _ = builder.ToTable("user", "dbo")
            .HasKey(user => user.UserId);

        _ = builder.HasOne(user => user.Role)
            .WithMany()
            .HasForeignKey(user => user.RoleId);

        _ = builder.Property(user => user.UserId)
            .HasColumnName("user_id")
            .IsRequired();
        _ = builder
            .Property(user => user.Name)
            .HasColumnName("name")
            .HasMaxLength(50)
            .IsRequired();
        _ = builder
            .Property(user => user.Password)
            .HasColumnName("password")
            .HasMaxLength(50)
            .IsRequired();
        _ = builder
            .Property(user => user.Email)
            .HasColumnName("email")
            .HasMaxLength(50)
            .IsRequired();
        _ = builder
            .Property(user => user.RoleId)
            .HasColumnName("role_id")
            .IsRequired();
        _ = builder
            .Property(user => user.IsActive)
            .HasColumnName("is_active")
            .IsRequired();
        _ = builder
            .Property(user => user.Phone)
            .HasColumnName("phone")
            .HasMaxLength(50)
            .IsRequired();
    }
}