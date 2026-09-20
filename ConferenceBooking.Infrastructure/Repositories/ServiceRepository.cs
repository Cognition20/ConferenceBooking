using ConferenceBooking.Application.Interfaces.Repositories;
using ConferenceBooking.Domain.Models;
using ConferenceBooking.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace ConferenceBooking.Infrastructure.Repositories;

public class ServiceRepository(AppDbContext dbContext) : IServiceRepository
{
    public async Task<Service?> GetServiceByName(string name)
    {
        return await dbContext.Services.FirstOrDefaultAsync(service => service.Name == name);
    }

    public async Task AddService(Service service, CancellationToken cancellationToken)
    {
        await dbContext.Services.AddAsync(service, cancellationToken);
    }

    public void DeleteService(Service service)
    {
        dbContext.Services.Remove(service);
    }

    public async Task<Service?> GetById(Guid? serviceId, CancellationToken cancellationToken)
    {
        return await dbContext.Services.FirstOrDefaultAsync(service => service.Id == serviceId, cancellationToken);
    }
}