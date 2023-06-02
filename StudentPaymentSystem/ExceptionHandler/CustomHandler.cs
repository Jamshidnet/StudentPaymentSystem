using Newtonsoft.Json;
using Serilog;

namespace StudentPaymentSystem.ExceptionHandler
{
    public class CustomHandler
    {
        private readonly RequestDelegate _next;

        public CustomHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                Log.Fatal("error occured. ");
                await HandleExceptionMessageAsync(httpContext, ex).ConfigureAwait(false);
            }
        }
        private static Task HandleExceptionMessageAsync(HttpContext context, Exception exception)
        {

            context.Response.ContentType = "application/json";
            int statusCode = 404;
            var result = JsonConvert.SerializeObject(new
            {
                StatusCode = statusCode,
                ErrorMessage = exception.Message + " this is inner exception"
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomHandlerExtensions
    {
        public static IApplicationBuilder UseCustomHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomHandler>();
        }
    }


}

