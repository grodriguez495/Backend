using System.Reflection;
using AirQualityControlAPI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AirQualityControlAPI.Infrastructure.Contexts;

public class DbContextMediator: IDbContextMediator
{
    public void ApplyConfigurations(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(),
            t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IAirQualityControlSchemaEntityTypeConfiguration<>)));
    }
}