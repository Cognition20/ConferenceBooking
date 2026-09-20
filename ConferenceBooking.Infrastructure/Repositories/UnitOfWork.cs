using ConferenceBooking.Application.Interfaces.Repositories;
using ConferenceBooking.Infrastructure.Persistance;

namespace ConferenceBooking.Infrastructure.Repositories;

public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await  dbContext.SaveChangesAsync(cancellationToken);
    }
}