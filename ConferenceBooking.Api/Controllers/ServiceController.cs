using ConferenceBooking.Application.Dtos.Requests;
using ConferenceBooking.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceBooking.Api.Controllers;

[Route("service")]
public class ServiceController(IServiceService serviceService) : ApiController
{
    [HttpPost("createService")]
    public async Task<IActionResult> CreateService(CreateServiceRequest serviceRequest, CancellationToken cancellationToken)
    {
        var result = await serviceService.CreateService(serviceRequest, cancellationToken);
        return Ok(result);
    }
}