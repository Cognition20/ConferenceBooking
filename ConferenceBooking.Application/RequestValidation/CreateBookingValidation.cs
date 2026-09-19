using ConferenceBooking.Application.Dtos.Requests;
using FluentValidation;

namespace ConferenceBooking.Application.RequestValidation;

public class CreateBookingValidation : AbstractValidator<CreateBookingRequest>
{
    public CreateBookingValidation()
    {
        RuleFor(x => x.StartAtUtc)
            .NotEmpty().WithMessage("StartAtUtc is required")
            .GreaterThan(DateTime.UtcNow.AddMinutes(-1)).WithMessage("StartAtUtc must be greater than today's date");
        
        RuleFor(x => x.EndAtUtc)
            .NotEmpty().WithMessage("EndAtUtc is required")
            .LessThan(DateTime.UtcNow.AddDays(7)).WithMessage("7 Days is maximum to booking");
    }
}
