using System.Globalization;
using System.Reflection;
using ONLINE_SHOP.Domain.Framework.Exceptions;
using ONLINE_SHOP.Domain.Framework.Helpers;
using ONLINE_SHOP.Domain.Framework.Services;
using ONLINE_SHOP.Infrastructure.Framework.HttpRouting;
using ONLINE_SHOP.Infrastructure.Framework.Tools;
using ONLINE_SHOP.Infrastructure.Framework.Tools.MediatR;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ONLINE_SHOP.Infrastructure.Framework.Extensions;

/// <summary>
/// Extensions to scan for MediatR handlers and registers them.
/// - Scans for any handler interface implementations and registers them as <see cref="ServiceLifetime.Transient"/>
/// - Scans for any <see cref="IRequestPreProcessor{TRequest}"/> and <see cref="IRequestPostProcessor{TRequest,TResponse}"/> implementations and registers them as transient instances
/// Registers <see cref="ServiceFactory"/> and <see cref="IMediator"/> as transient instances
/// After calling AddMediatR you can use the container to resolve an <see cref="IMediator"/> instance.
/// This does not scan for any <see cref="IPipelineBehavior{TRequest,TResponse}"/> instances including <see cref="RequestPreProcessorBehavior{TRequest,TResponse}"/> and <see cref="RequestPreProcessorBehavior{TRequest,TResponse}"/>.
/// To register behaviors, use the <see cref="ServiceCollectionServiceExtensions.AddTransient(IServiceCollection,Type,Type)"/> with the open generic or closed generic types.
/// </summary>
public static class ServiceCollectionExtensions
{
    public static void DynamicInject(this IServiceCollection services, IConfiguration configuration, string nameSpace)
    {
        var typesToRegister = AssemblyScanner.AllTypes(nameSpace, "(Infrastructure)+")
            .Where(it => !(it.IsAbstract || it.IsInterface) && typeof(IHaveInjection).IsAssignableFrom(it))
            .ToList();
        typesToRegister.AddRange(AssemblyScanner.AllTypes(nameSpace, "(Application)+")
            .Where(it => !(it.IsAbstract || it.IsInterface) && typeof(IHaveInjection).IsAssignableFrom(it)));

        foreach (var item in typesToRegister)
        {
            var service = (IHaveInjection) Activator.CreateInstance(item);
            try
            {
                service?.Inject(services, configuration);
            }
            catch // (Exception exception)
            {
                // exception.Log();
            }
        }
    }

    /// <summary>
    /// Registers handlers and mediator types from the specified assemblies
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="assemblies">Assemblies to scan</param>        
    /// <returns>Service collection</returns>
    public static IServiceCollection AddMediatR(this IServiceCollection services, params Assembly[] assemblies)
        => services.AddMediatR(assemblies, configuration: null);

    /// <summary>
    /// Registers handlers and mediator types from the specified assemblies
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="assemblies">Assemblies to scan</param>
    /// <param name="configuration">The action used to configure the options</param>
    /// <returns>Service collection</returns>
    public static IServiceCollection AddMediatR(this IServiceCollection services,
        Action<MediatRServiceConfiguration> configuration, params Assembly[] assemblies)
        => services.AddMediatR(assemblies, configuration);

    /// <summary>
    /// Registers handlers and mediator types from the specified assemblies
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="assemblies">Assemblies to scan</param>
    /// <param name="configuration">The action used to configure the options</param>
    /// <returns>Service collection</returns>
    public static IServiceCollection AddMediatR(this IServiceCollection services, IEnumerable<Assembly> assemblies, Action<MediatRServiceConfiguration> configuration)
    {
        var all = assemblies?.ToList() ?? new List<Assembly>();
        if (!all.Any())
            throw new Dexception(Situation.Make(SitKeys.Forbidden),
                new List<KeyValuePair<string, string>> {new(":پیام:", "امکان آماده‌سازی سیستم وجود ندارد.")});

        var serviceConfig = new MediatRServiceConfiguration();
        configuration?.Invoke(serviceConfig);
        MediatRServiceRegistrar.AddRequiredServices(services, serviceConfig);
        MediatRServiceRegistrar.AddMediatRClasses(services, all);

        return services;
    }

    /// <summary>
    /// Registers handlers and mediator types from the assemblies that contain the specified types
    /// </summary>
    /// <param name="services"></param>
    /// <param name="handlerAssemblyMarkerTypes"></param>        
    /// <returns>Service collection</returns>
    public static IServiceCollection AddMediatR(this IServiceCollection services, params Type[] handlerAssemblyMarkerTypes)
        => services.AddMediatR(handlerAssemblyMarkerTypes, configuration: null);

    public static IServiceCollection AddMediatR(this IServiceCollection services, string nameSpace)
    {
        var handlerAssemblyMarkerTypes = AssemblyScanner.AllTypes(nameSpace, "(.*)")
            .Where(it => !(it.IsAbstract || it.IsInterface));
        return services.AddMediatR(handlerAssemblyMarkerTypes, configuration: null);
    }

    /// <summary>
    /// Registers handlers and mediator types from the assemblies that contain the specified types
    /// </summary>
    /// <param name="services"></param>
    /// <param name="handlerAssemblyMarkerTypes"></param>
    /// <param name="configuration">The action used to configure the options</param>
    /// <returns>Service collection</returns>
    public static IServiceCollection AddMediatR(this IServiceCollection services,
        Action<MediatRServiceConfiguration> configuration, params Type[] handlerAssemblyMarkerTypes)
        => services.AddMediatR(handlerAssemblyMarkerTypes, configuration);

    /// <summary>
    /// Registers handlers and mediator types from the assemblies that contain the specified types
    /// </summary>
    /// <param name="services"></param>
    /// <param name="handlerAssemblyMarkerTypes"></param>
    /// <param name="configuration">The action used to configure the options</param>
    /// <returns>Service collection</returns>
    public static IServiceCollection AddMediatR(this IServiceCollection services,
        IEnumerable<Type> handlerAssemblyMarkerTypes,
        Action<MediatRServiceConfiguration> configuration)
        => services.AddMediatR(handlerAssemblyMarkerTypes.Select(t => t.GetTypeInfo().Assembly), configuration);

    public static void AddCustomLocalization(this IServiceCollection services)
    {
        services.Configure<RequestLocalizationOptions>(options =>
        {
            var cultures = new List<CultureInfo>
            {
                new("en-US"),
                new("fa-IR")
            };

            options.RequestCultureProviders = new List<IRequestCultureProvider>
            {
                new QueryStringRequestCultureProvider(),
                new CookieRequestCultureProvider()
            };

            options.DefaultRequestCulture = new RequestCulture("fa-IR", "fa-IR");
            options.SupportedCultures = cultures;
            options.SupportedUICultures = cultures;

            var cultureInfo = new CultureInfo("fa-IR") {NumberFormat = {CurrencySymbol = "IRT"}};
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        });
    }

    public static void AddCustomSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.DocInclusionPredicate((docName, apiDesc) =>
            {
                if (!apiDesc.TryGetMethodInfo(out var methodInfo))
                {
                    return false;
                }

                if (methodInfo.DeclaringType == null) return false;
                var versions = methodInfo.DeclaringType
                    .GetCustomAttributes(true)
                    .OfType<ApiVersionAttribute>()
                    .SelectMany(a => a.Versions);

                return versions.Any(v => $"v{v.ToString()}" == docName);
            });
            options.SwaggerDoc("v1.0",
                new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "V1 API",
                    Description = "Space Travel WebApi",
                    TermsOfService = new Uri("http://alibaba.ir")
                });

            options.AddSecurityDefinition("Bearer", new()
            {
                Type = SecuritySchemeType.ApiKey,
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "Bearer"}
                    },
                    new[] {"readAccess", "writeAccess"}
                }
            });
        });
    }

    public static void AddCustomApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(v =>
        {
            v.ReportApiVersions = true;
            v.AssumeDefaultVersionWhenUnspecified = true;
            v.DefaultApiVersion = new ApiVersion(1, 0);
        });
        services.AddVersionedApiExplorer(o =>
        {
            o.GroupNameFormat = "'v'VVV";
            o.SubstituteApiVersionInUrl = true;
        });
    }

    public static void AddCustomControllers(this IServiceCollection services, string nameSpace)
    {
        services
            .AddHttpContextAccessor()
            .AddMediatR(nameSpace)
            .AddControllers(options => { options.Conventions.Add(new RouteTokenTransformerConvention(new SnakeCaseRouter())); })
            .AddNewtonsoftJson(options => { options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; })
            .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization();
    }

    public static void AddAnyCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options => options
            .AddPolicy("SpaceTravelCorsPolicy", builder =>
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()));
    }
}