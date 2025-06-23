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
        /// <remarks>
        /// This method returns a list of all customers available in the system. If no customers
        /// are found,  the method responds with a bad request status and an error message. The response is formatted 
        /// as an HTTP action result.
        /// </remarks>
        /// <returns>
        /// An <see cref="ActionResult"/> containing the list of customers if the operation is successful,  or a bad
        /// request response if no customers are found.
        /// </returns>
        [HttpGet("GetAllCustomers")]
        public async Task<ActionResult> GetAllCustomers()
        {
            var result = await _customerAppService.GetAllCustomersAsync();

            if (result.Count() == 0)
                return BadRequest("Failure to search for customers!");

            return Ok(result);
        }

        /// <summary>
        /// Updates the details of an existing customer.
        /// </summary>
        /// <remarks>
        /// This method asynchronously updates the customer information using the provided data.
        /// Ensure that <paramref name="idCostumer"/> corresponds to an existing customer.
        /// </remarks>
        /// <param name="idCostumer">The unique identifier of the customer to be updated.</param>
        /// <param name="customerViewModel">An object containing the updated customer details.</param>
        /// <returns>
        /// An <see cref="ActionResult"/> indicating the outcome of the operation.  Returns <see langword="Ok"/> if the
        /// update is successful, or <see langword="BadRequest"/> if the update fails.
        /// </returns>
        [HttpPut("EditCustomer")]
        public async Task<ActionResult> EditCustomer(Guid idCostumer, [FromBody] CustomerViewModel customerViewModel)
        {
            var result = await _customerAppService.UpdateCustomerAsync(idCostumer, customerViewModel);

            if (!result)
                return BadRequest("Failure to update customer.");

            return Ok("Customer updated successfully.");
        }

        /// <summary>
        /// Deletes a customer with the specified identifier.
        /// </summary>
        /// <remarks>
        /// This method performs an asynchronous operation to delete a customer.  Ensure that the
        /// <paramref name="idCostumer"/> corresponds to an existing customer.
        /// </remarks>
        /// <param name="idCostumer">The unique identifier of the customer to delete.</param>
        /// <returns>
        /// An <see cref="ActionResult"/> indicating the result of the operation.  Returns <see langword="Ok"/> if the
        /// customer was successfully deleted, or  <see langword="BadRequest"/> if the deletion failed.
        /// </returns>
        [HttpDelete("DeleteCustomer")]
        public async Task<ActionResult> DeleteCustomer(Guid idCostumer)
        {
            var result = await _customerAppService.DeleteCustomerAsync(idCostumer);
            if (!result)
                return BadRequest("Failure to delete customer.");

            return Ok("Customer deleted successfully.");
        }
    }
}
