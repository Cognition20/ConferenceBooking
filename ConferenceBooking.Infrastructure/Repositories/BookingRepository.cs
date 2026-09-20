using ConferenceBooking.Application.Interfaces.Repositories;
using ConferenceBooking.Domain.Models;
using ConferenceBooking.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace ConferenceBooking.Infrastructure.Repositories;

public class BookingRepository(AppDbContext dbContext) : IBookingRepository
{
    public async Task CreateBooking(Booking booking, CancellationToken cancellationToken)
    {
        await dbContext.Bookings.AddAsync(booking, cancellationToken);
    }

    public Task DeleteBooking(Booking booking, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IsBooked(Guid conferenceId, DateTime startAtUtc, DateTime EndAtUtc, CancellationToken cancellationToken)
    {
        return await dbContext.Bookings.AnyAsync(b =>
            b.ConferenceRoomId == conferenceId &&
            !b.IsCancelled &&
            b.StartAtUtc < EndAtUtc.ToUniversalTime() &&
            startAtUtc.ToUniversalTime() < b.EndAtUtc, cancellationToken);
    }
    
    public Task<bool> HasAnyBookingsAsync(Guid roomId, CancellationToken cancellationToken) =>
        dbContext.Bookings.AnyAsync(b => b.ConferenceRoomId == roomId, cancellationToken);
}