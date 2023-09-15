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
            .ConfigureWebHostDefaults(x => x.UseStartup<Startup>());
}



