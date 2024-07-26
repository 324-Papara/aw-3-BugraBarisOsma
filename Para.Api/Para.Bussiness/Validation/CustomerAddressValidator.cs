using FluentValidation;
using Para.Schema;

namespace Para.Bussiness.Validation;

public class CustomerAddressValidator: AbstractValidator<CustomerAddressRequest>
{
    public CustomerAddressValidator()
    {
        RuleFor(address => address.CustomerId)
            .NotEmpty()
            .WithMessage("Customer Id is required.");
        RuleFor(address => address.Country)
            .NotEmpty()
            .WithMessage("Country is required.")
            .MaximumLength(50)
            .WithMessage("Country cannot have more than 50 characters.");
        RuleFor(address => address.City)
            .NotEmpty()
            .WithMessage("City is required.")
            .MaximumLength(50)
            .WithMessage("City cannot have more than 50 characters.");
        RuleFor(address => address.AddressLine)
            .NotEmpty()
            .WithMessage("Address line is required.")
            .MaximumLength(250)
            .WithMessage("Country cannot have more than 250 characters.");
        RuleFor(address => address.ZipCode)
            .NotEmpty()
            .MaximumLength(6)
            .WithMessage("zip code cannot have more than 6 characters.");
        RuleFor(address => address.IsDefault)
            .NotEmpty()
            .WithMessage("IsDefault property cannot be null.");
    }
}