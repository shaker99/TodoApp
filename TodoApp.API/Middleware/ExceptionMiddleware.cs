
using System.Net;
using System.Text.Json;


public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext); 
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Something went wrong");

            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";

            var errorResponse = new
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = "Internal Server Error Please try again later."
            };

            var errorJson = JsonSerializer.Serialize(errorResponse);
            await httpContext.Response.WriteAsync(errorJson);
        }
    }
}
