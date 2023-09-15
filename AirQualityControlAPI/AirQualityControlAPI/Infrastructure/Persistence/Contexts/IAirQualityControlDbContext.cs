using AirQualityControlAPI.Application.Interfaces;
using AirQualityControlAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AirQualityControlAPI.Infrastructure.Persistence.Contexts
{
    public interface IAirQualityControlDbContext : IBaseDbContext
    {
        DbSet<Role> Role { get; }
    }
}
