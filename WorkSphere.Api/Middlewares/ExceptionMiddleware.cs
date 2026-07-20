using System.Text.Json;
using WorkSphere.Application.Exceptions;

namespace WorkSphere.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,

                _ => StatusCodes.Status500InternalServerError
            };

            context.Response.ContentType = "application/json";

            var response = new
            {
                Message = ex.Message
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));

        }
    }
}