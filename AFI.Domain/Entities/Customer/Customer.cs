using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFI.Domain.Entities.Customer
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PolicyRefNumber { get; set; }
        public DateTime? DOB { get; set; }
        public string? Email { get; set; }
    }
}
