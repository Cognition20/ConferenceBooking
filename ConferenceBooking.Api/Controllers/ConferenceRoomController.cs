using ConferenceBooking.Application.Dtos.Requests;
using ConferenceBooking.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceBooking.Api.Controllers;

[Route("conferenceRoom")]
public class ConferenceRoomController(IConferenceRoomService conferenceRoomService) : ApiController
{
    [HttpPost("createRoom")]
    public async Task<IActionResult> CreateRoom(CreateConferenceRoomRequest request,
        CancellationToken cancellationToken)
    {
        var result = await conferenceRoomService.CreateRoom(request, cancellationToken);
        return Ok(result);
    }

    [HttpPut("updateRoom/{id:guid}")]
    public async Task<IActionResult> UpdateRoom(Guid id, UpdateConferenceRoomRequest request,
        CancellationToken cancellationToken)
    {
        var result = await conferenceRoomService.UpdateRoom(id, request, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("deleteRoom/{id:guid}")]
    public async Task<IActionResult> DeleteRoom(Guid id, CancellationToken cancellationToken)
    {
        await conferenceRoomService.DeleteRoom(id, cancellationToken);
        return NoContent();
    }

    [HttpGet("getRooms")]
    public async Task<IActionResult> GetAvailableRooms([FromQuery] SearchAvaibleRoomsRequest request, CancellationToken cancellationToken)
    {
        var result = await conferenceRoomService.GetAvailableRooms(request, cancellationToken);
        return Ok(result);
    }
}