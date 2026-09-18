using System.Net;
using System.Text.Json;
using ConferenceBooking.Domain.Exceptions;

namespace ConferenceBooking.Api.Middleware;


public sealed class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var (statusCode, message) = Map(ex);

            if (statusCode == HttpStatusCode.InternalServerError)
                logger.LogError(ex, "Unhandled exception while processing {Method} {Path}", context.Request.Method, context.Request.Path);
            else
                logger.LogWarning("Request {Method} {Path} failed: {Message}", context.Request.Method, context.Request.Path, message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var payload = JsonSerializer.Serialize(new { error = message });
            await context.Response.WriteAsync(payload);
        }
    }

    private static (HttpStatusCode StatusCode, string Message) Map(Exception ex) => ex switch
    {
        RoomNotFoundException or ServiceNotFoundException => (HttpStatusCode.NotFound, ex.Message),
        BookingConflictException => (HttpStatusCode.Conflict, ex.Message),
        ServiceNotAvailableForRoomException or InvalidBookingRequestException or ArgumentException
            => (HttpStatusCode.BadRequest, ex.Message),
        DomainException => (HttpStatusCode.BadRequest, ex.Message),
        _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred. Please try again later.")
    };
}
