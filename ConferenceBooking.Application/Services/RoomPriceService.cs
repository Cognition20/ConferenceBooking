using ConferenceBooking.Application.Interfaces.Services;

namespace ConferenceBooking.Application.Services;

public class RoomPriceService : IRoomPriceService
{
    public decimal GetRoomPrice(decimal pricePerHour, DateTime startAtUtc, DateTime endAtUtc)
    {
        if (endAtUtc <= startAtUtc)
            throw new ArgumentException("End time must be after start time.");
        
        var boundaries = new SortedSet<DateTime> { startAtUtc, endAtUtc };

        (TimeSpan From, TimeSpan To)[] periodBounds =
        {
            (TimeSpan.FromHours(6), TimeSpan.FromHours(9)),
            (TimeSpan.FromHours(9), TimeSpan.FromHours(12)),
            (TimeSpan.FromHours(12), TimeSpan.FromHours(14)),
            (TimeSpan.FromHours(14), TimeSpan.FromHours(18)),
            (TimeSpan.FromHours(18), TimeSpan.FromHours(23)),
        };

        for (var day = startAtUtc.Date; day <= endAtUtc.Date; day = day.AddDays(1))
        {
            foreach (var (from, to) in periodBounds)
            {
                var fromInstant = day + from;
                var toInstant = day + to;
                if (fromInstant > startAtUtc && fromInstant < endAtUtc) boundaries.Add(fromInstant);
                if (toInstant > startAtUtc && toInstant < endAtUtc) boundaries.Add(toInstant);
            }
        }

        var points = boundaries.ToList();
        decimal total = 0m;

        for (var i = 0; i < points.Count - 1; i++)
        {
            var segmentStart = points[i];
            var segmentEnd = points[i + 1];
            var midpoint = segmentStart + TimeSpan.FromTicks((segmentEnd - segmentStart).Ticks / 2);

            var hours = (decimal)(segmentEnd - segmentStart).TotalHours;
            total += hours * pricePerHour * GetMultiplier(midpoint.TimeOfDay);
        }

        return Math.Round(total, 2);
    }

    private static decimal GetMultiplier(TimeSpan timeOfDay)
    {
        if (timeOfDay >= TimeSpan.FromHours(6) && timeOfDay < TimeSpan.FromHours(9))
            return 0.90m; 

        if (timeOfDay >= TimeSpan.FromHours(12) && timeOfDay < TimeSpan.FromHours(14))
            return 1.15m;

        if (timeOfDay >= TimeSpan.FromHours(18) && timeOfDay < TimeSpan.FromHours(23))
            return 0.80m; 

        return 1.00m; 
    }
}