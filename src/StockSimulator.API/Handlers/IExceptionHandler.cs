using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockSimulator.API.Handlers
{
    public interface IExceptionHandler
    {
        Task HandleExceptionAsync(HttpContext context, Exception exception);
        
    }
}