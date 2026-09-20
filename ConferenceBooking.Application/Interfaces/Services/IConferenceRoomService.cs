using ConferenceBooking.Application.Dtos;
using ConferenceBooking.Application.Dtos.Requests;
using ConferenceBooking.Application.Dtos.Responses;
using ConferenceBooking.Domain.Models;

namespace ConferenceBooking.Application.Interfaces.Services;

public interface IConferenceRoomService
{
    Task<ConferenceRoomResponse> CreateRoom(CreateConferenceRoomRequest request, CancellationToken cancellationToken);
    Task<ConferenceRoomResponse> UpdateRoom(Guid id, UpdateConferenceRoomRequest request, CancellationToken cancellationToken);
    Task DeleteRoom(Guid conferenceRoomId, CancellationToken cancellationToken);
    Task<List<ConferenceRoomResponse>> GetAvailableRooms( SearchAvailableRoomsRequest request,CancellationToken cancellationToken);
}