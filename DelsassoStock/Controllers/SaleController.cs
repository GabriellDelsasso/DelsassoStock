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
        /// Creates a new sale based on the provided sale details.
        /// </summary>
        /// <remarks>
        /// Ensure that the <paramref name="saleViewModel"/> contains valid and complete data for
        /// the sale.  If the data is invalid or incomplete, the method will return a <see langword="BadRequest"/>
        /// response.
        /// </remarks>
        /// <param name="saleViewModel">The sale details to be created, provided as a <see cref="SaleViewModel"/> object.</param>
        /// <returns>
        /// An <see cref="ActionResult"/> indicating the result of the operation.  Returns <see langword="Ok"/> if the
        /// sale is successfully created, or <see langword="BadRequest"/> if the creation fails.
        /// </returns>
        [HttpPost("CreateSale")]
        public async Task<ActionResult> CreateSale([FromBody] SaleViewModel saleViewModel)
        {
            var result = await _saleAppService.CreateSale(saleViewModel);

            if (!result)
                return BadRequest("Failed to create sale. Please check the provided data.");

            return Ok("Sale created successfully.");
        }

        /// <summary>
        /// Retrieves a list of all sales records.
        /// </summary>
        /// <remarks>
        /// This method returns a collection of sales data in the form of <see
        /// cref="SaleResultViewModel"/> objects. The data is fetched asynchronously from the underlying sales
        /// application service.
        /// </remarks>
        /// <returns>
        /// An <see cref="ActionResult{T}"/> containing a list of <see cref="SaleResultViewModel"/> objects representing
        /// the sales records. Returns an empty list if no sales records are found.
        /// </returns>
        [HttpGet("GetAllSales")]
        public async Task<ActionResult<List<SaleResultViewModel>>> GetAllSales()
        {
            var sales = await _saleAppService.GetAllSalesAsync();
            return Ok(sales);
        }

        /// <summary>
        /// Updates an existing sale with the specified details.
        /// </summary>
        /// <remarks>
        /// Ensure that the <paramref name="id"/> corresponds to an existing sale and that 
        /// <paramref name="saleViewModel"/> contains valid data. Invalid or incomplete data  may result in a failed
        /// update.
        /// </remarks>
        /// <param name="id">The unique identifier of the sale to be updated.</param>
        /// <param name="saleViewModel">The updated sale details provided in the request body.</param>
        /// <returns>
        /// An <see cref="ActionResult"/> indicating the result of the operation.  Returns <see langword="Ok"/> if the
        /// sale was updated successfully;  otherwise, returns <see langword="BadRequest"/> with an error message if the
        /// update fails.
        /// </returns>
        [HttpPut("UpdateSale")]
        public async Task<ActionResult> UpdateSale(Guid id, [FromBody] SaleViewModel saleViewModel)
        {
            var result = await _saleAppService.UpdateSale(id, saleViewModel);
            if (!result)
                return BadRequest("Failed to update sale. Please check the provided data.");

            return Ok("Sale updated successfully.");
        }

        /// <summary>
        /// Deletes a sale identified by the specified ID.
        /// </summary>
        /// <remarks>
        /// Ensure that the <paramref name="id"/> corresponds to an existing sale.  If the sale
        /// does not exist or the deletion fails, a bad request response is returned.
        /// </remarks>
        /// <param name="id">The unique identifier of the sale to be deleted.</param>
        /// <returns>
        /// An <see cref="ActionResult"/> indicating the result of the operation.  Returns <see cref="OkObjectResult"/>
        /// if the sale was successfully deleted,  or <see cref="BadRequestObjectResult"/> if the deletion failed.
        /// </returns>
        [HttpDelete("DeleteSale")]
        public async Task<ActionResult> DeleteSale(Guid id)
        {
            var result = await _saleAppService.DeleteSale(id);

            if (!result)
                return BadRequest("Failed to delete sale. Please check the provided data.");

            return Ok("Sale deleted successfully.");
        }
    }
}
