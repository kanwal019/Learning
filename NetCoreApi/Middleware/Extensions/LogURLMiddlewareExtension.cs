using Microsoft.AspNetCore.Builder;

namespace NetCoreApi.Middleware.Extensions
{
    public static class LogURLMiddlewareExtension
    {
        public static IApplicationBuilder UseLogUrl(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LogURLMiddleware>();
        }
    }
}
