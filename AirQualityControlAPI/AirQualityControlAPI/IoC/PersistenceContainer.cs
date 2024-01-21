using AirQualityControlAPI.Domain.Repositories.Alerts.Commands;
using AirQualityControlAPI.Domain.Repositories.Alerts.Queries;
using AirQualityControlAPI.Domain.Repositories.AlertTypes.Commands;
using AirQualityControlAPI.Domain.Repositories.AlertTypes.Queries;
using AirQualityControlAPI.Domain.Repositories.Roles;
using AirQualityControlAPI.Domain.Repositories.Roles.Commands;
using AirQualityControlAPI.Domain.Repositories.Roles.Queries;
using AirQualityControlAPI.Domain.Repositories.Sensors.Commands;
using AirQualityControlAPI.Domain.Repositories.Sensors.Queries;
using AirQualityControlAPI.Domain.Repositories.Users.Commands;
using AirQualityControlAPI.Domain.Repositories.Users.Queries;
using AirQualityControlAPI.Infrastructure.Contexts;
using AirQualityControlAPI.Infrastructure.Persistence.Contexts;
using AirQualityControlAPI.Infrastructure.Repositories;
using Autofac;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AirQualityControlAPI.IoC;

public static class PersistenceContainer
{
    public static void RegisterPersistence(this ContainerBuilder builder)
    {
        RegisterContexts(builder);
        RegisterAirQualityContextRepositories(builder);
    }

    private static void RegisterAirQualityContextRepositories(ContainerBuilder builder)
    {
        builder.RegisterType<RoleRepository>()
            .As<IRoleQueryRepository>()
            .As<IRoleCommandRepository>()
            .InstancePerLifetimeScope();

        builder.RegisterType<UserRepository>()
            .As<IUserQueryRepository>()
            .As<IUserCommandRepository>()
            .InstancePerLifetimeScope();

        builder.RegisterType<SensorRepository>()
            .As<ISensorQueryRepository>()
            .As<ISensorCommandRepository>()
            .InstancePerLifetimeScope();
        builder.RegisterType<AlertsRepository>()
            .As<IAlertsQueryRepository>()
            .As<IAlertsCommandRepository>()
            .InstancePerLifetimeScope();
        
        builder.RegisterType<AlertTypeRepository>()
            .As<IAlertTypesQueryRepository>()
            .As<IAlertTypesCommandRepository>()
            .InstancePerLifetimeScope();
    }

    private static void RegisterContexts(ContainerBuilder builder)
    {
        builder.RegisterType<DbContextMediatorFactory>().As<IDbContextMediatorFactory>()
            .InstancePerLifetimeScope();

        builder.RegisterType<DbContextMediator>().As<IDbContextMediator>()
            .InstancePerLifetimeScope();

        RegisterAirQualityControlContext<AirQualityControlDbContext, IAirQualityControlDbContext>(builder,
            (ctxOptions, contextMediator) => new AirQualityControlDbContext(ctxOptions, contextMediator));
    }

    private static void RegisterAirQualityControlContext<TAirQualityControlContext, TIAirQualityControlContext>(ContainerBuilder builder,
        Func<DbContextOptions<TAirQualityControlContext>, IDbContextMediator, TAirQualityControlContext> ctxFunc)
    where TAirQualityControlContext: DbContext
    {
        builder.Register(c =>
            {
                var config = c.Resolve<IConfiguration>();
                var connString = config.GetConnectionString("ConnectionStringDb");
                if (string.IsNullOrWhiteSpace(connString))
                    throw new ArgumentNullException("ConnectionStrings:ConnectionStringDb");

                var opt = new DbContextOptionsBuilder<TAirQualityControlContext>();
                opt.UseSqlServer(connString,sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure();
                });

                return ctxFunc(opt.Options, c.Resolve<IDbContextMediator>());
            })
            .As<TIAirQualityControlContext>()
            .As<TAirQualityControlContext>()
            .InstancePerLifetimeScope();
    }
}