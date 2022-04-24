using AFI.Domain.Entities.Customer;
using FluentValidation;
using System.Text.RegularExpressions;

namespace AFI.Application.Services
{
    public  class CustomerValidator : AbstractValidator<Customer>
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
                .When(customer => string.IsNullOrEmpty(customer.Email));
            RuleFor(customer => customer.Email)
                .Must((customer, email) =>
                {
                    if (string.IsNullOrEmpty(email) || !email.Contains("@"))
                    {
                        return false;
                    }
                    var values = email.Split("@");
                    if (values[0].Length < 3)
                        return false;
                    var afterAtValue = values[1].Split(".");
                    if (afterAtValue[0].Length < 2)
                        return false;
                    return values[1].EndsWith(".com") || values[1].EndsWith("co.uk");
                }).WithMessage("Please enter valid Eamil")
                .When(customer => !customer.DOB.HasValue);
        }
    }
}
