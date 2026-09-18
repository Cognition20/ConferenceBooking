namespace ConferenceBooking.Domain.Models;

public class Booking
{
    public Guid Id { get; private set; }
    public Guid ConferenceRoomId { get; private set; }
    public ConferenceRoom? ConferenceRoom { get; private set; }
    public DateTime StartAtUtc { get; private set; }
    public DateTime EndAtUtc { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    private readonly List<Service> _services = new();
    public IReadOnlyCollection<Service> Services => _services.AsReadOnly();
    public decimal TotalPrice { get; private set; }
    public bool IsCancelled { get; private set; }

    public Booking(Guid conferenceRoomId, DateTime startAtUtc, DateTime endAtUtc, decimal totalPrice)
    {
        if (endAtUtc <= startAtUtc)
            throw new ArgumentOutOfRangeException(nameof(endAtUtc));
        
        Id = Guid.NewGuid();
        ConferenceRoomId = conferenceRoomId;
        StartAtUtc = startAtUtc;
        EndAtUtc = endAtUtc;
        TotalPrice = totalPrice;
        CreatedAtUtc = DateTime.UtcNow;
    }
    
    public TimeSpan Duration()
    {
        return EndAtUtc - StartAtUtc;
    }
    
    public  void AddService(Service service)
    {
        ArgumentNullException.ThrowIfNull(service);
        if (_services.Any(rs => rs.Id == service.Id))
            return;
        _services.Add(service);
    }
} 