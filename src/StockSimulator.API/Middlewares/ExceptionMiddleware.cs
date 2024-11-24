
using StockSimulator.API.Handlers;

namespace StockSimulator.API.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IExceptionHandler exceptionHandler)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ExceptionMiddleware> _logger = logger;
        private readonly IExceptionHandler _exceptionHandler = exceptionHandler;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"An error occurred: {ErrorMessage}", ex.Message);
                await _exceptionHandler.HandleExceptionAsync(context, ex);
            }
        }
    }
}
