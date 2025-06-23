using DelsassoStock.Application.Interfaces;
using DelsassoStock.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DelsassoStock.Controllers
{
    [Route("Sale")]
    public class SaleController : ControllerBase
    {
        private readonly ISaleAppService _saleAppService;

        public SaleController(ISaleAppService saleAppService)
        {
            _saleAppService = saleAppService;
        }

        /// <summary>
        /// Creates a new sale based on the provided sale data.
        /// </summary>
        /// <remarks>This method processes the sale creation request and returns an appropriate HTTP
        /// response. Ensure that the <paramref name="saleViewModel"/> contains all required fields and valid
        /// data.</remarks>
        /// <param name="saleViewModel">The sale data to be created, provided as a <see cref="SaleViewModel"/> object. This parameter must contain
        /// valid sale information.</param>
        /// <returns>An <see cref="ActionResult"/> indicating the result of the operation. Returns a 200 OK response with a
        /// success message if the sale is created successfully. Returns a 400 Bad Request response with an error
        /// message if the provided data is invalid. Returns a 500 Internal Server Error response with an error message
        /// if an unexpected error occurs.</returns>
        [HttpPost("CreateSale")]
        public async Task<ActionResult> CreateSale([FromBody] SaleViewModel saleViewModel)
        {
            try
            {
                var result = await _saleAppService.CreateSale(saleViewModel);

                if(result)
                {
                    return Ok(new ResultViewModel
                    {
                        Success = true,
                        Message = "Sale created successfully!",
                    });
                }
                return BadRequest(new ResultViewModel
                {
                    Success = false,
                    Message = "Failed to create sale. Please check the provided data."
                });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new ResultViewModel
                {
                    Success = false,
                    Message = "An error occurred while creating the sale.",
                    Data = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        /// <summary>
        /// Retrieves a list of all sales records.
        /// </summary>
        /// <remarks>This method returns a collection of sales data wrapped in a result view model.  If
        /// the operation is successful, the response contains the sales data along with a success message. In case of
        /// an error, the response includes an error message and a failure status.</remarks>
        /// <returns>An <see cref="ActionResult{T}"/> containing a <see cref="List{T}"/> of <see cref="SaleResultViewModel"/>
        /// objects if the operation is successful, or an error response if an exception occurs.</returns>
        [HttpGet("GetAllSales")]
        public async Task<ActionResult<List<SaleResultViewModel>>> GetAllSales()
        {
            try
            {
                var sales = await _saleAppService.GetAllSalesAsync();

                return Ok(new ResultViewModel
                {
                    Success = true,
                    Message = "Sales retrieved successfully.",
                    Data = sales
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel
                {
                    Success = false,
                    Message = "An error occurred while retrieving sales.",
                    Data = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        /// <summary>
        /// Updates an existing sale with the specified details.
        /// </summary>
        /// <remarks>This method uses the <c>HttpPut</c> attribute to handle HTTP PUT requests. Ensure
        /// that the <paramref name="id"/> corresponds to an existing sale and that  <paramref name="saleViewModel"/>
        /// contains valid data.</remarks>
        /// <param name="id">The unique identifier of the sale to be updated.</param>
        /// <param name="saleViewModel">The updated sale details provided in the request body.</param>
        /// <returns>An <see cref="ActionResult"/> indicating the result of the operation.  Returns <see cref="OkObjectResult"/>
        /// with a success message if the update is successful. Returns <see cref="NotFoundObjectResult"/> if the sale
        /// is not found or the update fails. Returns <see cref="StatusCodeResult"/> with status code 500 if an internal
        /// error occurs.</returns>
        [HttpPut("UpdateSale")]
        public async Task<ActionResult> UpdateSale(Guid id, [FromBody] SaleViewModel saleViewModel)
        {
            try
            {
                var result = await _saleAppService.UpdateSale(id, saleViewModel);

                if(result)
                {
                    return Ok(new ResultViewModel
                    {
                        Success = true,
                        Message = "Sale updated successfully!"
                    });
                }
                return NotFound(new ResultViewModel
                {
                    Success = false,
                    Message = "Sale not found or update failed."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel
                {
                    Success = false,
                    Message = "An error occurred while updating the sale.",
                    Data = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        /// <summary>
        /// Deletes a sale identified by the specified ID.
        /// </summary>
        /// <remarks>This method attempts to delete a sale record from the system based on the provided
        /// <paramref name="id"/>. If the sale is successfully deleted, a success response is returned. If the sale is
        /// not found or the deletion fails, a "Not Found" response is returned. In case of an unexpected error, a
        /// server error response is returned.</remarks>
        /// <param name="id">The unique identifier of the sale to be deleted. Must be a valid <see cref="Guid"/>.</param>
        /// <returns>An <see cref="ActionResult"/> indicating the outcome of the operation: <list type="bullet">
        /// <item><description>A success response if the sale is deleted successfully.</description></item>
        /// <item><description>A "Not Found" response if the sale does not exist or the deletion
        /// fails.</description></item> <item><description>A server error response if an unexpected error
        /// occurs.</description></item> </list></returns>
        [HttpDelete("DeleteSale")]
        public async Task<ActionResult> DeleteSale(Guid id)
        {
            try
            {
                var result = await _saleAppService.DeleteSale(id);

                if(result)
                {
                    return Ok(new ResultViewModel
                    {
                        Success = true,
                        Message = "Sale deleted successfully!"
                    });
                }
                return NotFound(new ResultViewModel
                {
                    Success = false,
                    Message = "Sale not found or deletion failed."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel
                {
                    Success = false,
                    Message = "An error occurred while deleting the sale.",
                    Data = ex.InnerException?.Message ?? ex.Message
                });
            }
        }
    }
}
