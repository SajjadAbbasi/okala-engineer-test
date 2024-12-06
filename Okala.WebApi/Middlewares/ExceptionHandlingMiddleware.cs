using System.Net;
using System.Text.Json;
using Okala.WebApi.Models;

namespace Okala.WebApi.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next,IWebHostEnvironment env
    ,ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // Log the exception (use any logging library)
        logger.LogError(exception,"Internal Server Error");

        // Set the response status code and content
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        if (env.IsDevelopment())
        {
            var response = new DevelopmentErrorModel(context.Response.StatusCode,
                "An unexpected error occurred. Please try again later.",
                exception.Message,
                exception.StackTrace
            );
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        else
        {
            var response = new ProductionErrorModel(context.Response.StatusCode,
                "An unexpected error occurred. Please try again later."
            );
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
      

    }
}