using Microsoft.AspNetCore.Builder;

namespace ProductService.Api.Middleware;

public static class LogURLMiddlewareExtention
{

    public class LogUrlMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogUrlMiddleware> _logger;

        public LogUrlMiddleware(RequestDelegate next, ILogger<LogUrlMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.LogInformation($"Request Url:{Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(context.Request)}");
            await this._next(context);
        }
    }

    public static IApplicationBuilder UseLogUrl(this IApplicationBuilder app)
    {
        app.UseMiddleware<LogUrlMiddleware>();
        return app;
    }
}
