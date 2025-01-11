using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CrmBackend.Utils;

public class CustomExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        context.Result = context.Exception is BadHttpRequestException castedException
            ? new ObjectResult(new { Error = context.Exception.Message }) { StatusCode = castedException.StatusCode }
            : new ObjectResult(new { Error = "Произошла внутренняя ошибка сервера" }) { StatusCode = 502 };

        context.ExceptionHandled = true;
    }
}
