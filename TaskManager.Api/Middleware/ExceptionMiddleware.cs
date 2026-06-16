using System.Net;
using TaskManager.Domain.Exceptions;

namespace TaskManager.Api.Middleware
{
    #region ExceptionMiddleware

    public class ExceptionMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Continua o pipeline normalmente.
                await _next(context);
            }
            catch (ValidationException ex)
            {
                // Erro de regra de negócio.
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception)
            {
                // Erro inesperado.
                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "An unexpected error occurred.");
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                error = message
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }

    #endregion ExceptionMiddleware
}