using GerencialClube.Dominio.Exceptions;
using System.Net;
using System.Text.Json;

namespace GerencialClube.WebApi.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger logger)
        {
            context.Response.ContentType = "application/json";

            var response = context.Response;
            var result = string.Empty;

            switch (exception)
            {
                case SocioException socioException:
                case PlanoException planoException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(new { mensagem = exception.Message });
                    break;

                default:
                    logger.LogError(exception, "Erro inesperado.");
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    result = JsonSerializer.Serialize(new
                    {
                        mensagem = "Erro interno no servidor.",
                        detalhe = exception.Message 
                    });
                    break;
            }

            await context.Response.WriteAsync(result);
        }
    }
}
