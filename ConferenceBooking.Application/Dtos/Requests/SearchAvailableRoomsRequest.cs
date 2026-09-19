namespace ConferenceBooking.Application.Dtos.Requests;

public record SearchAvailableRoomsRequest(
    DateTime StartUtc,
    DateTime EndUtc,
    int? MinCapacity);