namespace DatingApp.Api.Middleware
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _request;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate request, ILogger<GlobalExceptionMiddleware> logger)
        {
            _request = request;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _request(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                await HandleException(context, ex);
            }
        }

        private Task HandleException(HttpContext context, Exception exception)
        {
            var errorCode = nameof(HttpStatusCode.InternalServerError);
            var statusCode = HttpStatusCode.InternalServerError;
            var message = exception.Message;

            if (exception is UnauthorizedAccessException)
            {
                errorCode = nameof(HttpStatusCode.Unauthorized);
                statusCode = HttpStatusCode.Unauthorized;
            }
            //else if (exception is BlogException blogException)
            //{
            //    statusCode = blogException.ErrorCode.StatusCode;
            //    errorCode = blogException.ErrorCode.Message;
            //    message = string.IsNullOrEmpty(blogException.Message) ? errorCode : blogException.Message;
            //}

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            var responseBody = JsonConvert.SerializeObject(new { errorCode, message });

            return context.Response.WriteAsync(responseBody);
        }
    }
}
