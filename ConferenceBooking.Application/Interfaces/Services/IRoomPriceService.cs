namespace ConferenceBooking.Application.Interfaces.Services;

public interface IRoomPriceService
{
    decimal GetRoomPrice(decimal pricePerHour, DateTime startAtUtc, DateTime endAtUtc);
}