using ConferenceBooking.Application.Dtos.Requests;
using FluentValidation;

namespace ConferenceBooking.Application.RequestValidation;

public class CreateServiceValidation : AbstractValidator<CreateServiceRequest>
{
    public CreateServiceValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MinimumLength(3).WithMessage("Name must be greater than 3 characters")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters");
        
        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Price is required")
            .GreaterThan(0).WithMessage("PricePerHour must be greater than 0");
    }
}