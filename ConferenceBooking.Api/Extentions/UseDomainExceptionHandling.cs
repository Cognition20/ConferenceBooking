using ConferenceBooking.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceBooking.Api.Extentions;

public static class ExceptionHandlingExtensions
{
    public static IApplicationBuilder UseDomainExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                var (statusCode, title) = exception switch
                {
                    RoomNotFoundException or ServiceNotFoundException => (StatusCodes.Status404NotFound, exception.Message),
                    BookingConflictException => (StatusCodes.Status409Conflict, exception.Message),
                    RoomHasBookingsException => (StatusCodes.Status409Conflict, exception.Message),
                    DomainException => (StatusCodes.Status400BadRequest, exception.Message),
                    ArgumentException => (StatusCodes.Status400BadRequest, exception.Message),
                    _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
                };

                context.Response.StatusCode = statusCode;

                var problemDetailsService = context.RequestServices.GetRequiredService<IProblemDetailsService>();
                await problemDetailsService.WriteAsync(new ProblemDetailsContext
                {
                    HttpContext = context,
                    ProblemDetails = new ProblemDetails
                    {
                        Status = statusCode,
                        Title = title
                    }
                });
            });
        });
    }
}