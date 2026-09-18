using ConferenceBooking.Domain.Models;

namespace ConferenceBooking.Application.Interfaces.Repositories;

public interface IBookingRepository
{
    Task CreateBooking(Booking booking, CancellationToken cancellationToken);
    Task DeleteBooking(Booking booking, CancellationToken cancellationToken);
    Task<bool> IsBooked(Guid conferenceId, DateTime startAtUtc, DateTime endAtUtc, CancellationToken cancellationToken);
}