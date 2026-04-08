using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Cine.Filters;

public class ExceptionHandlingFilter: Attribute,IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is FormatException)
        {
            context.ExceptionHandled = true;
            context.Result = new ObjectResult(new { message = context.Exception.Message })
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
            return;
        }

        if (context.Exception is InvalidOperationException)
        {
            context.ExceptionHandled = true;
            context.Result = new ObjectResult(new { message = context.Exception.Message })
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
            return;
        }

        context.ExceptionHandled = true;
        context.Result = new ObjectResult(new { message = "Error inesperado en el servidor.", })
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
    }
}
