namespace ConferenceBooking.Application.Dtos.Requests;

public record CreateBookingRequest(
    Guid ConferenceId,
    DateTime StartAtUtc,
    DateTime EndAtUtc,
    List<Guid>? ServiceIds);