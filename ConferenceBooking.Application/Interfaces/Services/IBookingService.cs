using ConferenceBooking.Application.Dtos;
using ConferenceBooking.Application.Dtos.Requests;
using ConferenceBooking.Application.Dtos.Responses;

namespace ConferenceBooking.Application.Interfaces.Services;

public interface IBookingService
{
    Task<BookingResponse> CreateBooking(CreateBookingRequest request, CancellationToken cancellationToken);
}