using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace StockSimulator.API.Handlers
{
    public class ExceptionHandler : IExceptionHandler
    {
        public Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var statusCode = HttpStatusCode.InternalServerError;
            string? message;

            switch (exception)
            {
                case ArgumentNullException _:
                    statusCode = HttpStatusCode.BadRequest;
                    message = "A required argument was null.";
                    break;
                case ArgumentException _:
                    statusCode = HttpStatusCode.BadRequest;
                    message = "An invalid argument was provided.";
                    break;
                case InvalidOperationException _:
                    statusCode = HttpStatusCode.Conflict;
                    message = "The operation is invalid in the current state.";
                    break;
                case FormatException _:
                    statusCode = HttpStatusCode.BadRequest;
                    message = "An invalid format was provided.";
                    break;
                case KeyNotFoundException _:
                    statusCode = HttpStatusCode.NotFound;
                    message = "The requested resource was not found.";
                    break;
                case NotImplementedException _:
                    statusCode = HttpStatusCode.NotImplemented;
                    message = "The requested operation is not implemented.";
                    break;
                case UnauthorizedAccessException _:
                    statusCode = HttpStatusCode.Unauthorized;
                    message = "The request is unauthorized.";
                    break;                
                default:
                    message = exception.Message;
                    break;
            }

            var response = new
            {
                StatusCode = (int)statusCode,
                Message = message,
                Detailed = exception.Message
            };

            var jsonResponse = JsonSerializer.Serialize(response);

            return context.Response.WriteAsync(jsonResponse);
        }
    }
}
