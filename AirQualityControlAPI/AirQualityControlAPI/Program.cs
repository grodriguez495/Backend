using AirQualityControlAPI;


await Host.CreateDefaultBuilder(args)
.ConfigureWebHostDefaults(x => x.UseStartup<Startup>())
.Build()
.RunAsync();


