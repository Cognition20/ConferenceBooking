using ConferenceBooking.Application.Dtos.Requests;

namespace ConferenceBooking.Application.Dtos.Responses;

public record ConferenceRoomResponse(
    Guid Id,
    string Name,
    int Capacity,
    List<ServiceResponse> Services,
    decimal PricePerHour);
    