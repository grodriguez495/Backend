using AirQualityControlAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AirQualityControlAPI.Application.Interfaces
{
    public interface IAirQualityControlDbContext : IBaseDbContext, IAirQualityControlDbContextHandler
    {
        DbSet<Role> Roles { get; }
    }
}
