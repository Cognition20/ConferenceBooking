namespace ConferenceBooking.Application.Dtos.Requests;

public record SearchAvaibleRoomsRequest(
    DateTime startUtc,
    DateTime endUtc,
    int? minCapacity);