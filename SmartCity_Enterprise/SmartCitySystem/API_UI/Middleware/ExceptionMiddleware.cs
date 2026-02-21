using System.Net;
using System.Text.Json;


namespace API_UI.Middleware
{
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unexpected Error: {ex.Message}");
                await HandleExceptionAsync(context, ex);
            }

        }


        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = HttpStatusCode.InternalServerError;
            var message = "Unexpected error";


            if (exception is CityExceptionSystem || exception.InnerException is CityExceptionSystem)
            {
                statusCode = HttpStatusCode.BadRequest;

                message = exception is CityExceptionSystem ? exception.Message : exception.InnerException.Message;
            }

            else if (exception is LibraryLimitException || exception.InnerException is LibraryLimitException)
            {
                statusCode = HttpStatusCode.BadRequest;
                message = exception is LibraryLimitException ? exception.Message : exception.InnerException.Message;

            }
            else if (exception is DeviceLimitException || exception.InnerException is DeviceLimitException)
            {
                statusCode = HttpStatusCode.BadRequest;
                message = exception is DeviceLimitException ? exception.Message : exception.InnerException.Message;
            }

            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = message,
                Detailed = exception.Message 
            };

            var json = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(json);
        }

    }
}
