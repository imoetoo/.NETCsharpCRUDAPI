using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using TodoAPI.Models;

//Implements a mechanism for handling exceptions globally.  (part of service layer)
//This ensures that any exceptions that occur during the execution of our application are caught and handled appropriately, providing meaningful error messages to the client.

namespace TodoAPI.Middleware
{
    public class GlobalExceptionHandler : IExceptionHandler //IExceptionHandler interface is implemented by the GlobalExceptionHandler class, enabling global exception handling.
    {
        //readonly suggests that once the logger is initially assigned a value, it cannot be changed.
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync( //Invoked upon an exception. Logs error msg, creates Error Response object and sets status code and title based on the exception type. Returns a consistent error response to the client.
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(
                $"An error occurred while processing your request: {exception.Message}");

            var errorResponse = new ErrorResponse
            {
                Message = exception.Message
            };

            switch (exception)
            {
                case BadHttpRequestException:
                    errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Title = exception.GetType().Name;
                    break;

                default:
                    errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Title = "Internal Server Error";
                    break;
            }

            httpContext.Response.StatusCode = errorResponse.StatusCode;

            await httpContext
                .Response
                .WriteAsJsonAsync(errorResponse, cancellationToken);

            return true;
        }
    }
}