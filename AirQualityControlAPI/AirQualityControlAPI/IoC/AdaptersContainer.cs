using AirQualityControlAPI.Application.Interfaces;
using AirQualityControlAPI.Infrastructure.Persistence.Mapping;
using Autofac;
using System.Diagnostics.CodeAnalysis;

namespace AirQualityControlAPI.IoC
{
    public static class AdaptersContainer
    {
        [ExcludeFromCodeCoverage]
        public static void RegisterAppAdapters(this ContainerBuilder builder) 
        {
            builder.RegisterType<AutoMapperAdapter>().As<IAppMapper>().SingleInstance();
        }

    }
}
