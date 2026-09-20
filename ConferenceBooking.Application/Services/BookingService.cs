using System.Data;
using ConferenceBooking.Application.Dtos;
using ConferenceBooking.Application.Dtos.Requests;
using ConferenceBooking.Application.Dtos.Responses;
using ConferenceBooking.Application.Interfaces.Repositories;
using ConferenceBooking.Application.Interfaces.Services;
using ConferenceBooking.Domain.Exceptions;
using ConferenceBooking.Domain.Models;

namespace ConferenceBooking.Application.Services;

public class BookingService(IBookingRepository bookingRepository, 
    IConferenceRoomRepository conferenceRoomRepository, 
    IServiceRepository  serciceRepository,
    IUnitOfWork unitOfWork, 
    IRoomPriceService roomPriceService) 
    : IBookingService
{
    public async Task<BookingResponse> CreateBooking(CreateBookingRequest request, CancellationToken cancellationToken)
    {
        if (request.StartAtUtc < DateTime.UtcNow.AddMinutes(-1))
            throw new InvalidBookingRequestException("Cannot create a booking in the past.");
        
        var room = await conferenceRoomRepository.GetRoomById(request.ConferenceId, cancellationToken);
    
        if (room is null)
            throw new RoomNotFoundException(request.ConferenceId);
        

        if (await bookingRepository.IsBooked(request.ConferenceId, request.StartAtUtc, request.EndAtUtc,
                cancellationToken))
            throw new BookingConflictException(room.Id, request.StartAtUtc, request.EndAtUtc);
        
        var services = new List<Service>();

        if (request.ServiceIds is not null)
        {
            foreach (var serviceId in request.ServiceIds.Distinct())
            {
                var roomService = room.Services.FirstOrDefault(rs => rs.Id == serviceId);
                if (roomService is null)
                    throw new ServiceNotAvailableForRoomException(serviceId, room.Id);
                
                services.Add(roomService);
            }            
        }
        
        var roomPrice = roomPriceService.GetRoomPrice(
            room.PricePerHour,
            request.StartAtUtc,
            request.EndAtUtc);
        
        var servicesPrice = services.Sum(s => s.Price);

        var totalPrice = roomPrice + servicesPrice;

        var booking = new Booking(
            room.Id,
            request.StartAtUtc,
            request.EndAtUtc,
            totalPrice
        );
        foreach (var service in services)
        {
            booking.AddService(service);
        }

        await bookingRepository.CreateBooking(booking, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(booking);
    }

    private BookingResponse MapToDto(Booking booking)
    {
        return new BookingResponse(
            booking.Id,
            true,
            booking.TotalPrice);
    }
}
