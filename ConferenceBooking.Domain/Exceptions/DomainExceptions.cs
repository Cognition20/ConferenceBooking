namespace ConferenceBooking.Domain.Exceptions;

public abstract class DomainException : Exception
{
    protected DomainException(string message) : base(message) { }
}

public sealed class RoomNotFoundException : DomainException
{
    public RoomNotFoundException(Guid roomId) : base($"Room '{roomId}' was not found.") { }
}

public sealed class ServiceNotFoundException : DomainException
{
    public ServiceNotFoundException(Guid serviceId) : base($"Service '{serviceId}' was not found.") { }
}

public sealed class ServiceNotAvailableForRoomException : DomainException
{
    public ServiceNotAvailableForRoomException(Guid serviceId, Guid roomId)
        : base($"Service '{serviceId}' is not available for room '{roomId}'.") { }
}

public sealed class BookingConflictException : DomainException
{
    public BookingConflictException(Guid roomId, DateTime startUtc, DateTime endUtc)
        : base($"Room '{roomId}' is already booked for part of the interval {startUtc:o} - {endUtc:o}.") { }
}

public sealed class InvalidBookingRequestException : DomainException
{
    public InvalidBookingRequestException(string message) : base(message) { }
}