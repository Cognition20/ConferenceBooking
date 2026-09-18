using ConferenceBooking.Domain.Models;

namespace ConferenceBooking.Application.Dtos.Responses;

public record BookingResponse(
    bool Success,
    decimal TotalPrice);