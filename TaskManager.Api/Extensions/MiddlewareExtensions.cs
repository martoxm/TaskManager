using TaskManager.Api.Middleware;

namespace TaskManager.Api.Extensions
{
    #region MiddlewareExtensions

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder app)
        {
            // Registra o middleware global de exceção.
            return app.UseMiddleware<ExceptionMiddleware>();
        }
    }

    #endregion
}