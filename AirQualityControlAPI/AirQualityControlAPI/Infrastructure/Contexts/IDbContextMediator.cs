using Microsoft.EntityFrameworkCore;

namespace AirQualityControlAPI.Infrastructure.Contexts;

public interface IDbContextMediator
{
    void ApplyConfigurations(ModelBuilder builder);
}