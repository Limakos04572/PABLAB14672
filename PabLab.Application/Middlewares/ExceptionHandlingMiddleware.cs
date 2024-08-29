using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using PabLab.Domain.Exceptions.Abstractions;

namespace PabLab.Application.Middlewares;

public class ExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var statusCode = GetStatusCode(ex);
        var response = new
        {
            title = ex.Message,
            status = statusCode,
            errors = GetErrors(ex)
        };
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private int GetStatusCode(Exception ex)
        => ex switch
        {
            BaseException => (int)(ex as BaseException).StatusCode,
            ValidationException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };

    private IReadOnlyDictionary<string, string[]> GetErrors(Exception ex)
    {
        IReadOnlyDictionary<string, string[]> errors = null;

        if (ex is ValidationException validationException)
        {
            errors = validationException.Errors.ToDictionary(x => x.PropertyName, x => x.ErrorMessage.Split(","));
        }

        return errors;
    }
}