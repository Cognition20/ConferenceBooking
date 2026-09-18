namespace ConferenceBooking.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken  cancellationToken);
}