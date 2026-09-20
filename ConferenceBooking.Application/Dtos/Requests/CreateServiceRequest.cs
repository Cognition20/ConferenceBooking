namespace ConferenceBooking.Application.Dtos.Requests;

public record CreateServiceRequest(
    string Name,
    decimal Price);