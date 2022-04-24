using AFI.Domain.Entities.Customer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFI.Infrastructure
{
    public interface IAFIDbContext
    {
        DbSet<Customer> Customers { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
