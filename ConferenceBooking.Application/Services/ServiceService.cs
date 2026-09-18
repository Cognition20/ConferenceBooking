using ConferenceBooking.Application.Dtos;
using ConferenceBooking.Application.Dtos.Requests;
using ConferenceBooking.Application.Dtos.Responses;
using ConferenceBooking.Application.Interfaces.Repositories;
using ConferenceBooking.Application.Interfaces.Services;
using ConferenceBooking.Domain.Models;

namespace ConferenceBooking.Application.Services;

public class ServiceService(IServiceRepository serviceRepository, IUnitOfWork unitOfWork) : IServiceService
{
    public async Task<ServiceResponse> CreateService(CreateServiceRequest request, CancellationToken cancellationToken)
    {
        var service = new Service(
            request.Name,
            request.Price);
        
        await serviceRepository.AddService(service, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return MapToDto(service);
    }

    private ServiceResponse MapToDto(Service service)
    {
        return new ServiceResponse(service.Name, service.Price);
    }
}