using ConferenceBooking.Application.Dtos.Requests;
using ConferenceBooking.Application.Dtos.Responses;

namespace ConferenceBooking.Application.Interfaces.Services;

public interface IServiceService
{
    Task<ServiceResponse> CreateService(CreateServiceRequest request,  CancellationToken cancellationToken);
}