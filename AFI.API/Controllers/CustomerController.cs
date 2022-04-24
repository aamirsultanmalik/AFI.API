using AFI.Application.Services;
using AFI.Domain.Entities.Customer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AFI.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("GetAllCustomers")]
        public IActionResult Get()
        {
            return Ok(_customerService.GetAllCustomers());
        }

        [HttpPost("RegisterCustomer")]
        public async Task<IActionResult> Insert(Customer customer)
        {
            return Ok(await _customerService.RegisterCustomer(customer));
        }
    }
}
