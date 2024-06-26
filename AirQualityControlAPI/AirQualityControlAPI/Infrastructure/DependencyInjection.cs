﻿using AirQualityControlAPI.Application.Interfaces;
using AirQualityControlAPI.Domain.Models;
using AirQualityControlAPI.Domain.Repositories.Roles;
using AirQualityControlAPI.Infrastructure.Persistence.Mapping;
using AirQualityControlAPI.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using AirQualityControlAPI.Application.Features.Notifications;
using AirQualityControlAPI.Domain.Repositories.Alerts.Queries;
using AirQualityControlAPI.Domain.Repositories.Roles.Queries;
using AirQualityControlAPI.Domain.Repositories.Sensors.Queries;
using AirQualityControlAPI.Domain.Repositories.Users.Queries;
using AirQualityControlAPI.Infrastructure.Persistence.Contexts;


namespace AirQualityControlAPI.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            _ = services.AddDbContext<AirQualityControlDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("ConnectionStrings:ConnectionStringDb")));

            _ = services.AddScoped<IMediator, Mediator>();
            _ = services.AddScoped<IRoleQueryRepository, RoleRepository>();
            _ = services.AddScoped<IAlertsQueryRepository, AlertRepository>();
            _ = services.AddScoped<IUserQueryRepository, UserRepository>();
            _ = services.AddScoped<ISensorQueryRepository, SensorRepository>();
            _ = services.AddScoped<ISendNotification, SendNotification>();
            _ = services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
