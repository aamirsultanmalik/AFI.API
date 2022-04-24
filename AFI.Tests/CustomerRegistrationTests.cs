using System;
using System.Threading.Tasks;
using AFI.Application.Services;
using AFI.Application.Services.Customer.ViewModels;
using AFI.Application.Services.CustomerReg;
using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AFI.Tests
{
    public class CustomerRegistrationTests : IClassFixture<DependencySetupFixture>
    {
        private ICustomerService CustomerService { get; }
        private readonly CustomerValidator _customerValidator;
        public CustomerRegistrationTests(DependencySetupFixture fixture)
        {
            CustomerService = fixture.ServiceProvider.GetRequiredService<ICustomerService>();
            _customerValidator = new CustomerValidator();
        }

        [Fact]
        public async Task GivenCustomerRegData_WhenDataIsValid_ShouldAddNewRecord()
        {
            // Setup
            var customerReg = new CustomerViewModel
            {
                FirstName = "Joe",
                LastName = "Brunt",
                Email = "",
                DOB = DateTime.UtcNow.AddYears(-18),
                PolicyRefNumber = "XX-999999"
            };

            var result = await CustomerService.RegisterCustomer(customerReg);
            result.Should().BeGreaterThan(0);
        }

        [Fact]
        public void GivenCustomerRegData_WhenFirstNameOrLastNameLessThanRequiredLength_ShouldThrowValidationError()
        {
            // Setup
            var customerReg = new CustomerViewModel
            {
                FirstName = "Jo",
                LastName = "Br",
                Email = "",
                DOB = DateTime.UtcNow.AddYears(-18),
                PolicyRefNumber = "XX-999999"
            };
            var result = _customerValidator.TestValidate(customerReg);

            result.ShouldHaveValidationErrorFor(customer => customer.FirstName);
            result.ShouldHaveValidationErrorFor(customer => customer.LastName);
        }

        [Fact]
        public void GivenCustomerRegData_WhenAgeIsLessThan18_ShouldThrowValidationError()
        {
            // Setup
            var customerReg = new CustomerViewModel
            {
                FirstName = "Joe",
                LastName = "Brent",
                Email = "",
                DOB = DateTime.UtcNow.AddYears(-15),
                PolicyRefNumber = "XX-999999"
            };
            var result = _customerValidator.TestValidate(customerReg);

            result.ShouldHaveValidationErrorFor(customer => customer.DOB);
        }


        [Fact]
        public void GivenCustomerRegData_WhenAgeIs18OrAbove_ShouldPassValidation()
        {
            // Setup
            var customerReg = new CustomerViewModel
            {
                FirstName = "Joe",
                LastName = "Brent",
                Email = "",
                DOB = DateTime.UtcNow.AddYears(-19),
                PolicyRefNumber = "XX-999999"
            };
            var result = _customerValidator.TestValidate(customerReg);

            result.ShouldNotHaveValidationErrorFor(customer => customer.DOB);
        }

        [Fact]
        public void GivenCustomerRegData_WhenRefNumIsInIncorrectFormat_ShouldThrowValidationError()
        {
            // Setup
            var customerReg = new CustomerViewModel
            {
                FirstName = "Joe",
                LastName = "Brunt",
                Email = "",
                DOB = DateTime.UtcNow.AddYears(-18),
                PolicyRefNumber = "ads-9999d"
            };
            var result = _customerValidator.TestValidate(customerReg);

            result.ShouldHaveValidationErrorFor(customer => customer.PolicyRefNumber);
        }

        [Fact]
        public void GivenCustomerRegData_WhenRefNumIsInCorrectFormat_ShouldPassValidation()
        {
            // Setup
            var customerReg = new CustomerViewModel
            {
                FirstName = "Joe",
                LastName = "Brunt",
                Email = "",
                DOB = DateTime.UtcNow.AddYears(-18),
                PolicyRefNumber = "XX-999999"
            };
            var result = _customerValidator.TestValidate(customerReg);

            result.ShouldNotHaveValidationErrorFor(customer => customer.PolicyRefNumber);
        }

        [Fact]
        public void GivenCustomerRegData_WhenEmailIsInInCorrectFormat_ShouldThrowValidationError()
        {
            // Setup
            var customerReg = new CustomerViewModel
            {
                FirstName = "Joe",
                LastName = "Brunt",
                Email = "a@z.com",
                DOB = null,
                PolicyRefNumber = "XX-999999"
            };
            var result = _customerValidator.TestValidate(customerReg);

            result.ShouldHaveValidationErrorFor(customer => customer.Email);
        }


        [Fact]
        public void GivenCustomerRegData_WhenEmailIsInCorrectFormat_ShouldPassValidation()
        {
            // Setup
            var customerReg = new CustomerViewModel
            {
                FirstName = "Joe",
                LastName = "Brunt",
                Email = "abcc@z2.com",
                DOB = null,
                PolicyRefNumber = "XX-999999"
            };
            var result = _customerValidator.TestValidate(customerReg);

            result.ShouldNotHaveValidationErrorFor(customer => customer.Email);
        }
    }
}
