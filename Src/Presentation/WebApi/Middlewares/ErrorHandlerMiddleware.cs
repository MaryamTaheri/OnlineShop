using System.Diagnostics;
using System.Net;
using System.Text.Json;
using ONLINE_SHOP.Domain.Framework.Exceptions;
using ONLINE_SHOP.Infrastructure.Framework.HttpResults.Contracts;

namespace ONLINE_SHOP.Presentation.WebApi.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next, IConfiguration configuration, IHostEnvironment environment)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await Handle(context, exception);
        }
    }

    private async Task Handle(HttpContext httpContext, Exception ex)
    {
        httpContext.Response.ContentType = "application/problem+json";

        // Get the details to display, depending on whether we want to expose the raw exception
        var key = "UNHANDLED_ERROR";
        var title = "خطای پیش بینی نشده: " + ex.Message;
        var description = ex.StackTrace;

        // This is often very handy information for tracing the specific request
        var httpCode = HttpStatusCode.InternalServerError;
        int? situationCode = null;
        var errors = new Dictionary<string, object>();

        if (ex is Dexception dex)
        {
            key = dex.Key;
            title = dex.Message;
            description = dex.Description;
            httpCode = dex.HttpCode;
            situationCode = dex.SituationCode;
        }

        var i = 1;
        var iex = ex.InnerException;
        while (iex != null)
        {
            errors.Add($"خطای داخلی شماره {i++} : ", iex.Message + "\n\n" + iex.StackTrace);
            iex = iex.InnerException;
        }

        var problem = new StandardForcedResponseContract
        {
            Result = new StandardForcedResultContract
            {
                Title = key,
                Message = title,
                Description = description,
                Status = situationCode ?? 0,
                Errors = errors
            }
        };

        httpContext.Response.StatusCode = (int) httpCode;
        var stream = httpContext.Response.Body;
        await JsonSerializer.SerializeAsync(stream, problem, new JsonSerializerOptions(JsonSerializerDefaults.Web));
    }
}

public static class ErrorHandlerExtensions
{
    public static void UseCustomErrorHandler(this IApplicationBuilder app)
    {
        //app.UseMiddleware<ErrorHandlerMiddleware>();
    }
}