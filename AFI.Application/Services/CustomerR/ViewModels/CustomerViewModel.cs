using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFI.Application.Services.Customer.ViewModels
{
    
    public class CustomerListViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PolicyRefNumber { get; set; }
        public DateTime? DOB { get; set; }
        public string? Email { get; set; }
    }
    public class CustomerViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PolicyRefNumber { get; set; }
        public DateTime? DOB { get; set; }
        public string? Email { get; set; }

        [NotMapped]
        public bool EmailHasValue
        {
            get
            {
                return !string.IsNullOrEmpty(Email);
            }
        }

        [NotMapped]
        public bool DobHasValue
        {
            get
            {
                return DOB.HasValue;
            }
        }
    }


}
