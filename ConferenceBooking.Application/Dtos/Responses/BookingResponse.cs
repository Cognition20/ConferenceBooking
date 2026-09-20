using ConferenceBooking.Domain.Models;

namespace ConferenceBooking.Application.Dtos.Responses;

public record BookingResponse(
    Guid Id,
    bool Success,
    decimal TotalPrice);