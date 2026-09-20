using ConferenceBooking.Application.Dtos.Requests;
using FluentValidation;

namespace ConferenceBooking.Application.RequestValidation;

public class CreateRoomValidation : AbstractValidator<CreateConferenceRoomRequest>
{
    public CreateRoomValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MinimumLength(3).WithMessage("Name must be greater than 3 characters")
            .MaximumLength(50).WithMessage("Name cannot exceed 50 characters");
        
        RuleFor(x => x.Capacity)
            .NotEmpty().WithMessage("Capacity is required")
            .InclusiveBetween(10, 5000).WithMessage("Capacity must be greater than 10 characters");

        RuleForEach(x => x.Services)
            .SetValidator(new CreateServiceValidation());
        
        RuleFor(x => x.PricePerHour)
            .NotEmpty().WithMessage("PricePerHour is required")
            .GreaterThan(0).WithMessage("PricePerHour must be greater than 0");
    }
}
