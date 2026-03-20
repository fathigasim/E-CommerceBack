using FluentValidation;  // ✅ Make sure this is here!

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace EcommerceApplication.Exceptions
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "An error occurred: {Message}", exception.Message);

            context.Response.ContentType = "application/json";

            object response;
            int statusCode;

            // ✅ Use fully qualified name to be 100% sure
            if (exception is FluentValidation.ValidationException validationException)
            {
                Console.WriteLine("✅ Matched FluentValidation.ValidationException");

                var modelState = new ModelStateDictionary();
                foreach (var error in validationException.Errors)
                {
                    Console.WriteLine($"   Adding: {error.PropertyName} = {error.ErrorMessage}");
                    modelState.AddModelError(
                        error.PropertyName ?? string.Empty,
                        error.ErrorMessage);
                }

                response = new ValidationProblemDetails(modelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "One or more validation errors occurred.",
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
                };
                statusCode = StatusCodes.Status400BadRequest;
            }
            else if (exception is NotFoundException notFoundException)
            {
                response = new ProblemDetails
                {
                    Title = notFoundException.Message,
                    Status = StatusCodes.Status404NotFound
                };
                statusCode = StatusCodes.Status404NotFound;
            }
            else if (exception is UnauthorizedAccessException)
            {
                response = new ProblemDetails
                {
                    Title = exception.Message,
                    Status = StatusCodes.Status401Unauthorized
                };
                statusCode = StatusCodes.Status401Unauthorized;
            }
            else
            {
                Console.WriteLine($"⚠️ Unhandled: {exception.GetType().FullName}");

                response = new ProblemDetails
                {
                    Title = "An internal server error occurred",
                    Status = StatusCodes.Status500InternalServerError
                };
                statusCode = StatusCodes.Status500InternalServerError;
            }

            context.Response.StatusCode = statusCode;

            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(json);
        }
    }
}