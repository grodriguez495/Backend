using AirQualityControlAPI.Application.Interfaces;
using AirQualityControlAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AirQualityControlAPI.Infrastructure.Persistence.Contexts
{
    public interface IAirQualityControlDbContext : IBaseDbContext
    {
        DbSet<Role> Role { get; }
        DbSet<User> User { get; }
        DbSet<Sensor> Sensor { get; }
        DbSet<AlertNotification> Alerts { get; }
        DbSet<AlertType> AlertTypes { get; }
    }
}
