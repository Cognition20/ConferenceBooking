using ConferenceBooking.Domain.Models;

namespace ConferenceBooking.Application.Interfaces.Repositories;

public interface IServiceRepository
{
    Task<Service?> GetServiceByName(string name);
    Task AddService(Service service, CancellationToken cancellationToken);
    void DeleteService(Service service);
    
    Task<Service?> GetById(Guid? serviceId, CancellationToken cancellationToken);

}