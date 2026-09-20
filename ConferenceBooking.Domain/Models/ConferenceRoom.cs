namespace ConferenceBooking.Domain.Models;

public class ConferenceRoom
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    private readonly List<Service> _services = new();
    public IReadOnlyCollection<Service> Services => _services.AsReadOnly();
    private readonly List<Booking> _bookings = new();
    public IReadOnlyCollection<Booking> Bookings => _bookings.AsReadOnly();
    public decimal PricePerHour { get; set; }

    public ConferenceRoom(string name, int capacity, decimal pricePerHour)
    {
        Id = Guid.NewGuid();
        Rename(name);
        SetCapacity(capacity);
        SetPricePerHour(pricePerHour);
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Room name cannot be empty.", nameof(name));
        Name = name.Trim();
    }

    public void SetCapacity(int capacity)
    {
        if (capacity <= 0)
            throw new ArgumentException("Room capacity must be a positive number.", nameof(capacity));
        Capacity = capacity;
    }

    public void SetPricePerHour(decimal pricePerHour)
    {
        if (pricePerHour <= 0)
            throw new ArgumentException("Base hourly rate must be a positive number.", nameof(pricePerHour));
        PricePerHour = pricePerHour;
    }

    public  void AddService(Service service)
    {
        ArgumentNullException.ThrowIfNull(service);
        if (_services.Any(rs => rs.Id == service.Id))
            return;
        _services.Add(service);
    }

    public void RemoveService(Guid serviceId)
    {
        var link = _services.FirstOrDefault(rs => rs.Id == serviceId);
        if (link is not null)
            _services.Remove(link);
    }
    
}