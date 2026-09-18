namespace ConferenceBooking.Application.Dtos.Requests;

public record UpdateConferenceRoomRequest(
    string? Name,
    int? Capacity,
    decimal? PricePerHour,
    List<CreateServiceRequest>? Services);