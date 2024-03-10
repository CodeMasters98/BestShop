using Newtonsoft.Json;
using System.Net;

namespace ProductService.Api.Middleware;

public static class GlobalExceptionHandlerExtention
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
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
                //Log To Database
                await HandleExceptionAsync(context: context, ex: ex);

            }
        }

        public static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                IsSuccessful = false,
                Error = new
                {
                    message = "An error occured",
                    details = ex.ToString()
                }
            };
            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }

    public static IApplicationBuilder UseGlobalException(this IApplicationBuilder app)
    {
        app.UseMiddleware<GlobalExceptionMiddleware>();
        return app;
    }
}
