using DelsassoStock.Application.Interfaces;
using DelsassoStock.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DelsassoStock.Controllers
{
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerAppService _customerAppService;

        public CustomerController(ICustomerAppService customerAppService)
        {
            _customerAppService = customerAppService;
        }

        [HttpPost("RegisterCustomer")]
        public async Task<ActionResult> RegisterCustomer(CustomerViewModel customerViewModel)
        { 
            var result = await _customerAppService.RegisterCustomerAsync(customerViewModel);

            if(result == null)
                return BadRequest("Invalid customer data.");

            return Ok(result);
        }
    }
}
