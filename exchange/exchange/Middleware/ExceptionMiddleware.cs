using Exchange.Web.Shared.Common;
using Exchange.Web.Shared.Enums;
using Exchange.Web.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Exchange.Web.Presentation.Middleware
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (UserException ex)
            {
                context.Response.StatusCode = (int)ex.Code;
                await context.Response.WriteAsync(
                $"Error:{(int)ex.Code} {(ex.Code).GetAttribute<EnumDescriptor>().Description}\n {ex.Description}");
            }
        }
    }
}
