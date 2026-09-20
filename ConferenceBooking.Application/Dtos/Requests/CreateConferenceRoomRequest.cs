namespace ConferenceBooking.Application.Dtos.Requests;

public record CreateConferenceRoomRequest(
    string Name,
    int Capacity,
    List<CreateServiceRequest>? Services,
    decimal PricePerHour);