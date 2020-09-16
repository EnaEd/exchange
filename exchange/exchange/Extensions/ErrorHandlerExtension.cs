using Exchange.Web.Presentation.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Exchange.Web.Presentation.Extensions
{
    public static class ErrorHandlerExtension
    {
        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
