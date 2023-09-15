using System.Reflection;
using Autofac;
using AutoMapper;

namespace AirQualityControlAPI.IoC;

public static class AutoMapperContainer
{
    public static void RegisterAutoMapper(this ContainerBuilder builder, params Assembly[] assembliesToScan)
    {
        if (assembliesToScan == null || assembliesToScan.Length == 0)
            return;

        var assembliesTypes = assembliesToScan.SelectMany(a => a.GetTypes())
            .Where(type => typeof(Profile).IsAssignableFrom(type) && type.IsPublic && !type.IsAbstract)
            .Distinct();

        var autoMapperProfiles = assembliesTypes.Select(p => (Profile) Activator.CreateInstance(p)).ToList();

        builder.Register(_ => new MapperConfiguration(cfg =>
        {
            foreach (var profile in autoMapperProfiles)
            {
                cfg.AddProfile(profile);
            }
        })).InstancePerLifetimeScope();

        builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>().InstancePerLifetimeScope();
    }
}