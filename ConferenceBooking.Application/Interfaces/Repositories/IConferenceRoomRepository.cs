using ConferenceBooking.Domain.Models;

namespace ConferenceBooking.Application.Interfaces.Repositories;

public interface IConferenceRoomRepository
{
    Task<IEnumerable<ConferenceRoom>> GetAllRoomsBySearch(DateTime date, TimeSpan timeSpan,int capacity);
    Task<ConferenceRoom?> GetRoomById(Guid id, CancellationToken cancellationToken);
    Task<ConferenceRoom> CreateRoom(ConferenceRoom conferenceRoom, CancellationToken cancellationToken);
    Task DeleteRoom(Guid id, CancellationToken cancellationToken);
    Task<List<ConferenceRoom>> GetAvailableRooms(DateTime startUtc, DateTime endUtc, int? minCapacity,CancellationToken cancellationToken);
}