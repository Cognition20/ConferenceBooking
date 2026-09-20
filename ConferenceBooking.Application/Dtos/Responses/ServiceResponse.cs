namespace ConferenceBooking.Application.Dtos.Responses;

public record ServiceResponse(
    Guid Id,
    string Name,
    decimal Price);