using ONLINE_SHOP.Domain.Framework.Events;
using ONLINE_SHOP.Domain.Framework.Exceptions;
using ONLINE_SHOP.Domain.Framework.Helpers;
using ONLINE_SHOP.Infrastructure.Framework.Events;
using ONLINE_SHOP.Infrastructure.Framework.Extensions;
using ONLINE_SHOP.Presentation.WebApi.Middlewares;

namespace ONLINE_SHOP.Presentation.WebApi;

public class Startup
{
    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
        GlobalConfig.Config = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCustomControllers("ONLINE_SHOP");

        services.AddCors(options => options.AddPolicy("SpaceTravelCorsPolicy", builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()));

        services.AddCustomApiVersioning();
        services.AddCustomSwagger();

        services.AddSingleton<IMessagePublisher, RabbitMqPublisher>();
        services.AddSingleton<BusHandler, RabbitMqBusHandler>();

        services.DynamicInject(Configuration, "ONLINE_SHOP");

        
            SetupConsumers(services);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
        
        app.UseCustomErrorHandler();

        app.UseCustomSwagger();

        if (env.IsProduction())
            app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseCustomLocalization();

        app.UseRouting();

        app.UseCustomCors();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

         ////
        app.EnsureSqliteDb();    
    }

    public void SetupConsumers(IServiceCollection services)
    {
        var serviceProvider = services?.BuildServiceProvider();
        
        if (serviceProvider == null)
            throw new Dexception(Situation.Make(SitKeys.Unprocessable), new List<KeyValuePair<string, string>>
            {
                new(":پیام:", "امکان دریافت لیست سرویس‌ها وجود ندارد.")
            });


        var service = serviceProvider.GetService<BusHandler>();
        
        if (service is null)
            throw new Dexception(Situation.Make(SitKeys.Unprocessable), new List<KeyValuePair<string, string>>
            {
                new(":پیام:", "امکان برقرار ارتباط با صف وجود ندارد.")
            });

        service.ExtractConsumers(serviceProvider, "ONLINE_SHOP");
        service.StartListening();
    }
}