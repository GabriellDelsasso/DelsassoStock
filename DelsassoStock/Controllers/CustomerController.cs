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
        /// Registers a new customer using the provided customer details.
        /// </summary>
        /// <remarks>This method accepts a <see cref="CustomerViewModel"/> containing customer information
        /// and attempts to register the customer in the system. If the registration is successful, a success message
        /// and the result are returned. If an error occurs, an appropriate HTTP status code and error message are
        /// returned.</remarks>
        /// <param name="customerViewModel">The customer details to be registered. This parameter cannot be <see langword="null"/>.</param>
        /// <returns>An <see cref="ActionResult"/> containing the result of the operation. If successful, the response includes a
        /// success message and the registered customer data. If an error occurs, the response includes an error message
        /// and the appropriate HTTP status code.</returns>
        [HttpPost("RegisterCustomer")]
        public async Task<ActionResult> RegisterCustomer(CustomerViewModel customerViewModel)
        {
            try
            {
                var result = await _customerAppService.RegisterCustomerAsync(customerViewModel);

                return Ok(new ResultViewModel
                {
                    Success = true,
                    Message = "Customer registered successfully!",
                    Data = result
                });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new ResultViewModel
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel
                {
                    Success = false,
                    Message = $"An error occurred while registering the customer",
                    Data = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        /// <summary>
        /// Retrieves a list of all customers.
        /// </summary>
        /// <remarks>This method returns a collection of customer data wrapped in a result view model.  If
        /// the operation is successful, the response contains the customer data and a success message.  In case of an
        /// error, the response includes an error message and a failure status.</remarks>
        /// <returns>An <see cref="ActionResult"/> containing a <see cref="ResultViewModel"/>.  If successful, the <see
        /// cref="ResultViewModel.Data"/> property contains the list of customers. If an error occurs, the <see
        /// cref="ResultViewModel.Data"/> property contains the error details.</returns>
        [HttpGet("GetAllCustomers")]
        public async Task<ActionResult> GetAllCustomers()
        {
            try
            {
                var result = await _customerAppService.GetAllCustomersAsync();

                return Ok(new ResultViewModel
                {
                    Success = true,
                    Message = "Customers retrieved successfully.",
                    Data = result
                });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new ResultViewModel
                {
                    Success = false,
                    Message = "An error occurred while retrieving customers.",
                    Data = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        /// <summary>
        /// Updates the details of an existing customer.
        /// </summary>
        /// <remarks>This method attempts to update the customer information based on the provided
        /// identifier and data. If the customer is not found or the update fails, a <see cref="NotFoundResult"/> is
        /// returned. If the input data is invalid, a <see cref="BadRequestResult"/> is returned. In case of an
        /// unexpected error, a <see cref="StatusCodeResult"/> with status code 500 is returned.</remarks>
        /// <param name="idCostumer">The unique identifier of the customer to be updated.</param>
        /// <param name="customerViewModel">An object containing the updated customer details. This parameter must not be null.</param>
        /// <returns>An <see cref="ActionResult"/> indicating the result of the operation: <list type="bullet">
        /// <item><description><see cref="OkResult"/> if the customer is successfully updated.</description></item>
        /// <item><description><see cref="NotFoundResult"/> if the customer is not found or the update
        /// fails.</description></item> <item><description><see cref="BadRequestResult"/> if the input data is
        /// invalid.</description></item> <item><description><see cref="StatusCodeResult"/> with status code 500 if an
        /// unexpected error occurs.</description></item> </list></returns>
        [HttpPut("EditCustomer")]
        public async Task<ActionResult> EditCustomer(Guid idCostumer, [FromBody] CustomerViewModel customerViewModel)
        {
            try
            {
                var result = await _customerAppService.UpdateCustomerAsync(idCostumer, customerViewModel);

                if(result)
                {
                    return Ok(new ResultViewModel
                    {
                        Success = true,
                        Message = "Customer updated successfully."
                    });
                }
                return NotFound(new ResultViewModel
                {
                    Success = false,
                    Message = "Customer not found or update failed."
                });
            }
            catch(ArgumentNullException ex)
            {
                return BadRequest(new ResultViewModel
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel
                {
                    Success = false,
                    Message = $"An error occurred while updating the customer",
                    Data = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        /// <summary>
        /// Deletes a customer identified by the specified ID.
        /// </summary>
        /// <remarks>This method performs the deletion asynchronously and handles potential errors. Ensure
        /// the provided <paramref name="idCostumer"/> corresponds to an existing customer.</remarks>
        /// <param name="idCostumer">The unique identifier of the customer to delete. Must be a valid <see cref="Guid"/>.</param>
        /// <returns>An <see cref="ActionResult"/> indicating the result of the operation.  Returns <see cref="OkObjectResult"/>
        /// with a success message if the customer is deleted successfully. Returns <see cref="NotFoundObjectResult"/>
        /// if the customer is not found or the deletion fails. Returns <see cref="StatusCodeResult"/> with status code
        /// 500 if an unexpected error occurs.</returns>
        [HttpDelete("DeleteCustomer")]
        public async Task<ActionResult> DeleteCustomer(Guid idCostumer)
        {
            try
            {
                var result = await _customerAppService.DeleteCustomerAsync(idCostumer);

                if(result)
                {
                    return Ok(new ResultViewModel
                    {
                        Success = true,
                        Message = "Customer deleted successfully."
                    });
                }
                return NotFound(new ResultViewModel
                {
                    Success = false,
                    Message = "Customer not found or deletion failed."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel
                {
                    Success = false,
                    Message = $"An error occurred while deleting the customer",
                    Data = ex.InnerException?.Message ?? ex.Message
                });
            }
        }
    }
}
