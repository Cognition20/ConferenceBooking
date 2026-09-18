namespace ConferenceBooking.Domain.Models;

public class Service
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    private readonly List<ConferenceRoom> _rooms = new();
    public IReadOnlyCollection<ConferenceRoom> Rooms => _rooms.AsReadOnly();

    public Service( string name, decimal price)
    {
        Id = Guid.NewGuid();
        Name = name;
        Price = price;
    }
}