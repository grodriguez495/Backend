using AirQualityControlAPI.Application.Reflection;
using Autofac;
using Autofac.Core;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace AirQualityControlAPI.IoC
{
    public class ContainerModule : Autofac.Module
    {
        [ExcludeFromCodeCoverage]
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterAppAdapters();
            builder.RegisterPersistence();
            var applicationAssembly = ApplicationAssemblyFinder.GetAssembly();
            builder.RegisterMediatR(applicationAssembly);
            var persistenceAssembly = PersistenceAssemblyFinder.GetAssembly();
            builder.RegisterAutoMapper(persistenceAssembly);
            builder.RegisterConfigs();
        }
    }
}
