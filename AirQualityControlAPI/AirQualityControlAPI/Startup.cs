//using Microsoft.AspNetCore.Authentication.JwtBearer;

using System.Reflection;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Primitives;
using AirQualityControlAPI.Infrastructure;
using Autofac;
using AirQualityControlAPI.IoC;
using MediatR;

namespace AirQualityControlAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddApplication();
            services.AddInfrastructure(Configuration);
            services.AddHttpContextAccessor();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddOptions();
            
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("api", new OpenApiInfo()
                {
                    Description = "AirQualityControl API with curd operations",
                    Title = "AirQualityControl",
                    Version = "1"
                });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .WithOrigins("http://localhost:4200", "http://localhost:82")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            #region codigo comentado

            /* services.AddDbContext<AirQualityControlDbContext>(options =>
                options.UseSqlServer(connectionString));*/
           
            //var _dbcontext = services.BuildServiceProvider().GetService<AirQualityControlDbContext>();

           

            //services.AddSingleton<IRefreshTokenGenerator>(provider => new RefreshTokenGenerator(_dbcontext));

            /*  var _jwtsetting = Configuration.GetSection("JWTSetting");
              services.Configure<JWTSetting>(_jwtsetting);*/

            //var authkey = Configuration.GetValue<string>("JWTSetting:securitykey");

            /* services.AddAuthentication(item =>
             {
                 item.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                 item.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
             }).AddJwtBearer(item =>
             {

                 item.RequireHttpsMetadata = true;
                 item.SaveToken = true;
                 item.TokenValidationParameters = new TokenValidationParameters()
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authkey)),
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     ValidateLifetime = true,
                     ClockSkew = TimeSpan.Zero
                 };
             });*/


            #endregion
        }

        public void ConfigureContainer(ContainerBuilder builder) 
        {
            builder.RegisterModule(new ContainerModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
       
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSwagger();

        
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    // Add CORS header to allow error message to be visible to Angular
                    if (context.Request.Headers.TryGetValue("Origin", out StringValues origin))
                    {
                        context.Response.Headers.Add("Access-Control-Allow-Origin", origin.ToString());
                    }
                });
            });

            app.UseSwaggerUI(options => options.SwaggerEndpoint("api/swagger.json", "AirQualityControl"));

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }

}
