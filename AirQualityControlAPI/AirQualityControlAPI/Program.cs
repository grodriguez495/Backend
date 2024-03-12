using AirQualityControlAPI;
using Autofac.Extensions.DependencyInjection;
using MediatR;

public class Program
{
    public async static Task Main(string[] args)
    {
        await CreateHostBuilder(args)
            .Build()
            .RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders(); // Limpiar los proveedores de logging existentes
                logging.AddConsole(); // Agregar el proveedor de logging para la consola
            })
            .ConfigureWebHostDefaults(x => x.UseStartup<Startup>());
}



