using FluentValidation;
using Para.Schema;

namespace Para.Bussiness.Validation;

public class CustomerDetailValidator : AbstractValidator<CustomerDetailRequest>
{
    public CustomerDetailValidator()
    {
        RuleFor(customerDetail => customerDetail.FatherName)
            .NotEmpty()
            .WithMessage("Father name is required.")
            .MaximumLength(50)
            .WithMessage("Father name cannot be greater than 50 characters.");
        RuleFor(customerDetail => customerDetail.MotherName)
            .NotEmpty()
            .WithMessage("Mother name is required.")
            .MaximumLength(50)
            .WithMessage("Mother name cannot be greater than 50 characters.");
        RuleFor(customerDetail => customerDetail.MontlyIncome)
            .NotEmpty()
            .WithMessage("Monthly income is required.")
            .MaximumLength(50)
            .WithMessage("Monthly income cannot be greater than 50 characters.");
        RuleFor(customerDetail => customerDetail.Occupation)
            .NotEmpty()
            .WithMessage("Occupation is required.")
            .MaximumLength(50)
            .WithMessage("Occupation cannot be greater than 50 characters.");
        RuleFor(customerDetail => customerDetail.EducationStatus)
            .NotEmpty()
            .WithMessage("Education status is required.")
            .MaximumLength(50)
            .WithMessage("Education status cannot be greater than 50 characters.");
    }
}