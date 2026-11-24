using ControleGastos.Application;
using ControleGastos.Domain;
using System.Text.Json;

namespace ControleGastos.Api;

public sealed class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            logger.LogWarning(exception, "Erro ao processar requisição");
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = exception switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                BudgetExceededException => StatusCodes.Status422UnprocessableEntity,
                DomainException => StatusCodes.Status400BadRequest,
                ArgumentException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(new { error = exception.Message }));
        }
    }
}
