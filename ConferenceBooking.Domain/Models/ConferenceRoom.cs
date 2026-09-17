namespace ConferenceBooking.Domain.Models;

public class ConferenceRoom
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public List<string> Services { get; set; }
    public decimal PricePerHour { get; set; }
}