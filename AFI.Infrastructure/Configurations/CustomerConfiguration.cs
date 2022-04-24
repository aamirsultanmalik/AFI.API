using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AFI.Domain.Entities.Customer;

namespace AFI.Infrastructure.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(customer => customer.Id);
            builder.Property(customer => customer.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(customer => customer.LastName).IsRequired().HasMaxLength(50);
            builder.Property(customer => customer.PolicyRefNumber).IsRequired().HasMaxLength(9);
            builder.Property(customer => customer.DOB);
            builder.Property(customer => customer.Email);

        }
    }
}
