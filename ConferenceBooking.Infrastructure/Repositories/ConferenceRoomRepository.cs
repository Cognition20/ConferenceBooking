using ConferenceBooking.Application.Interfaces.Repositories;
using ConferenceBooking.Domain.Models;
using ConferenceBooking.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace ConferenceBooking.Infrastructure.Repositories;

public class ConferenceRoomRepository(AppDbContext dbContext) : IConferenceRoomRepository
{
    public Task<IEnumerable<ConferenceRoom>> GetAllRoomsBySearch(DateTime date, TimeSpan timeSpan, int capacity)
    {
        throw new NotImplementedException();
    }

    public async Task<ConferenceRoom?> GetRoomById(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.ConferenceRooms.FirstOrDefaultAsync(c => c.Id == id,  cancellationToken);
    }

    public async Task<ConferenceRoom> CreateRoom(ConferenceRoom conferenceRoom, CancellationToken cancellationToken)
    {
        await dbContext.ConferenceRooms.AddAsync(conferenceRoom, cancellationToken);
        return conferenceRoom;
    }

    public async Task DeleteRoom(Guid id, CancellationToken cancellationToken)
    {
        var room = await GetRoomById(id, cancellationToken);
        if (room is not null)
        {
            dbContext.ConferenceRooms.Remove(room);
        }
        // return that room doesnt exists
    }

    public async Task<List<ConferenceRoom>> GetAvailableRooms(DateTime startUtc, DateTime endUtc, int? minCapacity, CancellationToken cancellationToken)
    {
        var query = dbContext.ConferenceRooms.AsQueryable();

        if (minCapacity is { } capacity)
            query = query.Where(r => r.Capacity >= capacity);

        query = query.Where(r => !r.Bookings.Any(b =>
            !b.IsCancelled &&
            b.StartAtUtc < endUtc &&
            startUtc < b.EndAtUtc));
        
        return await query.ToListAsync(cancellationToken);
    }
}