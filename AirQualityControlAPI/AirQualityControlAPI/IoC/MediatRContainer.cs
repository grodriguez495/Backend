using System.Diagnostics.CodeAnalysis;

using MediatR;
using System.Reflection;
using Autofac;

namespace AirQualityControlAPI.IoC
{
    public static class MediatRContainer
    {

        public static void RegisterMediatR(this ContainerBuilder builder, params Assembly[] assembliesToScan)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).Assembly)
                .AsImplementedInterfaces()
                .InstancePerDependency();


            builder.RegisterAssemblyTypes(assembliesToScan)
                .AsClosedTypesOf(typeof(IRequestHandler<,>))
                .InstancePerDependency() ;

           builder.RegisterAssemblyTypes(assembliesToScan)
                .AsClosedTypesOf(typeof(INotificationHandler<>))
                .InstancePerDependency() ;
        
         
        }
    }
}
