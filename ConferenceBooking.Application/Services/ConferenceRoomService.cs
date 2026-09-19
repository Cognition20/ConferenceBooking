using ConferenceBooking.Application.Dtos.Requests;
using ConferenceBooking.Application.Dtos.Responses;
using ConferenceBooking.Application.Interfaces.Repositories;
using ConferenceBooking.Application.Interfaces.Services;
using ConferenceBooking.Domain.Exceptions;
using ConferenceBooking.Domain.Models;

namespace ConferenceBooking.Application.Services;

public class ConferenceRoomService(
    IConferenceRoomRepository conferenceRoomRepository,
    IServiceRepository serviceRepository, IUnitOfWork unitOfWork) : IConferenceRoomService
{
    public async Task<ConferenceRoomResponse> CreateRoom(CreateConferenceRoomRequest request, CancellationToken cancellationToken)
    {
        var conRoom = new ConferenceRoom(request.Name, request.Capacity, request.PricePerHour);

        if (request.Services is not null)
        {
            foreach (var svc in request.Services)
                await AddServices(conRoom, svc, cancellationToken);
        }
        
        await conferenceRoomRepository.CreateRoom(conRoom, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return MapToDto(conRoom);
    }

    public async Task<ConferenceRoomResponse> UpdateRoom(Guid id, UpdateConferenceRoomRequest request, CancellationToken cancellationToken)
    {
        var room = await conferenceRoomRepository.GetRoomById(id, cancellationToken);

        if (room is null)
            throw new RoomNotFoundException(id);
        
        if(!string.IsNullOrWhiteSpace(request.Name))
            room.Rename(request.Name);
        
        if (request.Capacity is { } capacity)
            room.SetCapacity(capacity);

        if (request.PricePerHour is { } pricePerHour)
            room.SetPricePerHour(pricePerHour);

        if (request.Services is not null)
        {
            foreach (var svc in request.Services)
                await AddServices(room, svc, cancellationToken);
        }
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return MapToDto(room);
    }

    public async Task DeleteRoom(Guid conferenceRoomId, CancellationToken cancellationToken)
    {
        await conferenceRoomRepository.DeleteRoom(conferenceRoomId, cancellationToken);
    }

    public async Task<List<ConferenceRoomResponse>> GetAvailableRooms(SearchAvailableRoomsRequest request,
        CancellationToken cancellationToken)
    {
        if(request.StartUtc >= request.EndUtc)
            throw new InvalidBookingRequestException("Search end time must be after the start time.");
        
        var rooms = await conferenceRoomRepository.GetAvailableRooms(
            request.StartUtc, 
            request.EndUtc, 
            request.MinCapacity,
            cancellationToken);
        
        return rooms.Select(MapToDto).ToList();
    }
    

    private async Task AddServices(ConferenceRoom room, CreateServiceRequest request,
        CancellationToken cancellationToken)
    {
        var service = await serviceRepository.GetServiceByName(request.Name);

        if (service is null)
        {
            if (string.IsNullOrWhiteSpace(request.Name) || request.Price <= 0)
                throw new InvalidBookingRequestException(
                    "A service reference must include either an existing ServiceId or a Name and Price for a new service.");
                
            service = new Service(request.Name, request.Price);
            await serviceRepository.AddService(service, cancellationToken);
        }

        room.AddService(service);
    }
    
    private ConferenceRoomResponse MapToDto(ConferenceRoom conRoom)
    {
        return new ConferenceRoomResponse(
            conRoom.Id,
            conRoom.Name,
            conRoom.Capacity,
            conRoom.Services.Select(s => new ServiceResponse(s.Name, s.Price)).ToList(), 
            conRoom.PricePerHour);
    }
}