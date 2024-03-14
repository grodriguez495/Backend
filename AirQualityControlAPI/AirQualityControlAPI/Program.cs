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
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddUserSecrets<Startup>();
            })
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders(); 
                logging.AddConsole(); 
            })
            
            .ConfigureWebHostDefaults(x => x.UseStartup<Startup>());
}



