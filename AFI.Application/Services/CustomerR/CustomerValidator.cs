using AFI.Application.Services.Customer.ViewModels;
using AFI.Domain.Entities.Customer;
using FluentValidation;
using System.Text.RegularExpressions;

namespace AFI.Application.Services
{
    public class CustomerValidator : AbstractValidator<CustomerViewModel>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.FirstName).NotEmpty().MinimumLength(3).MaximumLength(50);
            RuleFor(customer => customer.LastName).NotEmpty().MinimumLength(3).MaximumLength(50);
            RuleFor(customer => customer.PolicyRefNumber).NotEmpty()
                .Matches(new Regex("^[A-Z]{2}-[0-9]{6}$"));
            RuleFor(customer => customer.DOB)
                .Must((customer, dob) =>
                {
                    if (!dob.HasValue || dob.Value >= DateTime.UtcNow)
                        return false;
                    var zeroTime = new DateTime(1, 1, 1);
                    var timeSpan = DateTime.UtcNow - dob.Value;
                    var years = (zeroTime + timeSpan).Year - 1;
                    return years >= 18;
                }).WithMessage("You must be older then 18 to register")
                .When(customer => !customer.EmailHasValue
                || (customer.DobHasValue && customer.EmailHasValue));

            RuleFor(customer => customer.Email)
                .Matches(new Regex(@"^[a-zA-Z0-9]{3}[\S\D]*@[a-zA-Z0-9]{1}[\S\D]*(.com|.co.uk)$"))
                .WithMessage("Please enter valid Eamil")
                .When(customer => !customer.DobHasValue
                || (customer.DobHasValue && customer.EmailHasValue));
        }
    }
}
