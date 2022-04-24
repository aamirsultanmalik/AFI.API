using AFI.Application.Services;
using AFI.Application.Services.Customer.ViewModels;
using AFI.Application.Services.CustomerReg;
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
        public async Task<ActionResult<List<CustomerListViewModel>>> Insert(CustomerViewModel customer)
        {
            return Ok(await _customerService.RegisterCustomer(customer));
        }
    }
}
