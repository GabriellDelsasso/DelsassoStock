using DelsassoStock.Application.Interfaces;
using DelsassoStock.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DelsassoStock.Controllers
{
    [Route("Customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerAppService _customerAppService;

        public CustomerController(ICustomerAppService customerAppService)
        {
            _customerAppService = customerAppService;
        }

        /// <summary>
        /// Registers a new customer using the provided customer data.
        /// </summary>
        /// <remarks>
        /// This method processes the customer registration asynchronously. Ensure that the
        /// provided  <paramref name="customerViewModel"/> contains valid and complete data to avoid errors.
        /// </remarks>
        /// <param name="customerViewModel">The customer data to register. This must include all required fields for customer registration.</param>
        /// <returns>
        /// An <see cref="ActionResult"/> indicating the result of the operation.  Returns <see langword="Ok"/> with the
        /// registration result if successful, or <see langword="BadRequest"/>  with an error message if the provided
        /// customer data is invalid.
        /// </returns>
        [HttpPost("RegisterCustomer")]
        public async Task<ActionResult> RegisterCustomer(CustomerViewModel customerViewModel)
        {
            var result = await _customerAppService.RegisterCustomerAsync(customerViewModel);

            if (result == null)
                return BadRequest("Invalid customer data.");

            return Ok(result);
        }

        /// <summary>
        /// Retrieves all customers from the system.
        /// </summary>
        /// <remarks>This method returns a list of all customers available in the system. If no customers
        /// are found,  the method responds with a bad request status and an error message. The response is formatted 
        /// as an HTTP action result.</remarks>
        /// <returns>An <see cref="ActionResult"/> containing the list of customers if the operation is successful,  or a bad
        /// request response if no customers are found.</returns>
        [HttpGet("GetAllCustomers")]
        public async Task<ActionResult> GetAllCustomers()
        {
            var result = await _customerAppService.GetAllCustomersAsync();

            if (result.Count() == 0)
                return BadRequest("Failure to search for customers!");

            return Ok(result);
        }
    }
}
