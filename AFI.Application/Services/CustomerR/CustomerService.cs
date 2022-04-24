using AFI.Infrastructure;
using AFI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using AFI.Application.Services.Customer.ViewModels;

namespace AFI.Application.Services.CustomerReg
{
    public class CustomerService : ICustomerService
    {
        private readonly IAFIDbContext _dbContext;
        public CustomerService(IAFIDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> RegisterCustomer (CustomerViewModel customer)
        {
            var cust = new Domain.Entities.Customer.Customer
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                DOB = customer.DOB,
                Email = customer.Email,
                PolicyRefNumber = customer.PolicyRefNumber
            };
            await _dbContext.Customers.AddAsync(cust);
            await _dbContext.SaveChangesAsync(CancellationToken.None);
            return cust.Id;
        }

        public IQueryable<CustomerListViewModel> GetAllCustomers()
        {
            return _dbContext.Customers.Select(c => new CustomerListViewModel
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                DOB = c.DOB,
                Email = c.Email,
                PolicyRefNumber = c.PolicyRefNumber

            }).AsNoTracking();
        }
    }

    public interface ICustomerService
    {
        Task<int> RegisterCustomer(CustomerViewModel customer);

        IQueryable<CustomerListViewModel> GetAllCustomers();
    }
}
