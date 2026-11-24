using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace NetCoreApi.Middleware;

public partial class LogURLMiddleware(RequestDelegate next, ILogger<LogURLMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        LogRequestUrl(logger, Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(context.Request));
        await context.Response.WriteAsync("Logger Middleware Invoked !!\n\n");
        await next(context);
    }

    [LoggerMessage(EventId = 1, Level = LogLevel.Information, Message = "Request URL: {Url}")]
    private static partial void LogRequestUrl(ILogger logger, string url);
}
