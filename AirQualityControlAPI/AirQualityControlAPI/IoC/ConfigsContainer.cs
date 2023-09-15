using Autofac;

namespace AirQualityControlAPI.IoC;

public static class ConfigsContainer
{
    public static void RegisterConfigs(this ContainerBuilder builder)
    {
        builder.Register(ctx =>
            {
                var config = ctx.Resolve<IConfiguration>();
                return config.GetSection("ConnectionStrings").Get<DataBaseConfig>();
            }).As<IDatabaseConfig>()
            .InstancePerLifetimeScope();
    }
}