using AFI.Domain.Entities.Customer;
using AFI.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFI.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IAFIDbContext _dbContext;
        public CustomerService(IAFIDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> RegisterCustomer (Customer customer)
        {
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync(CancellationToken.None);
            return customer.Id;
        }

        public IQueryable<Customer> GetAllCustomers()
        {
            return _dbContext.Customers.AsNoTracking();
        }
    }

    public interface ICustomerService
    {
        Task<int> RegisterCustomer(Customer customer);

        IQueryable<Customer> GetAllCustomers();
    }
}
