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
        
        _ = builder.Property(user => user.UserId)
            .HasColumnName("user_id");
        
        _ = builder.Property(user => user.Name)
            .HasColumnName("name").HasMaxLength(50).IsRequired();
        
        _ = builder.Property(user => user.Password)
            .HasColumnName("password").HasMaxLength(50).IsRequired();
        
        _ = builder.Property(user => user.Email)
            .HasColumnName("email").HasMaxLength(50).IsRequired();
        
        _ = builder
            .Property(role => role.RoleId)
            .HasColumnName("role_id");
        
        _ = builder.Property(user => user.IsActive)
            .HasColumnName("is_active");
        
        _ = builder.Property(user => user.Phone)
            .HasColumnName("phone").HasMaxLength(50).IsRequired();
    }
}