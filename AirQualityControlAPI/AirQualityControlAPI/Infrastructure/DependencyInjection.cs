using AirQualityControlAPI.Application.Interfaces;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.Roles;
using AirQualityControlAPI.Infrastructure.Persistence.Mapping;
using AirQualityControlAPI.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace AirQualityControlAPI.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            _ = services.AddDbContext<AirQualityControlDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("constring")));
            _ = services.AddScoped<IAirQualityControlDbContext>(serviceProvider => new AirQualityControlDbContext(
                serviceProvider.GetService<DbContextOptions>()));
            _ = services.AddAutoMapper(Assembly.GetExecutingAssembly());
            _ = services.AddScoped<IRoleQueryRepository, RoleRepository>();
            services.AddScoped<IMediator, Mediator>();
            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        

            return services;
        }
    }
}
