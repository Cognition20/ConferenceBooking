using ConferenceBooking.Application.Dtos.Requests;
using ConferenceBooking.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceBooking.Api.Controllers;

[Route("booking")]
public class BookingController(IBookingService bookingService) : ApiController
{

    [HttpPost("makeBooking")]
    public async Task<IActionResult> MakeBooking(CreateBookingRequest request, CancellationToken cancellationToken)
    {
        var result = await bookingService.CreateBooking(request, cancellationToken);
        return Ok(result);
    }
}