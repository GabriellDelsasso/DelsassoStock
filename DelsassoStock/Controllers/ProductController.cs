using DelsassoStock.Application.Interfaces;
using DelsassoStock.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DelsassoStock.Controllers
{
    [Route("Product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductAppService _productAppService;

        public ProductController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        /// <summary>
        /// Registers a new product using the provided product details.
        /// </summary>
        /// <remarks>This method handles product registration by delegating the operation to the
        /// application service. It returns an HTTP 200 status code for successful registrations and an HTTP 400 status
        /// code for errors.</remarks>
        /// <param name="productViewModel">The product details to register. This parameter must contain valid product information.</param>
        /// <returns>An <see cref="IActionResult"/> containing the result of the operation.  If successful, the response includes
        /// a success message and the registered product data. If the operation fails, the response includes an error
        /// message and details.</returns>
        [HttpPost("RegisterProduct")]
        public async Task<IActionResult> RegisterProduct(ProductViewModel productViewModel)
        {
            try
            {
                var result = await _productAppService.RegisterProduct(productViewModel);

                return Ok(new ResultViewModel
                {
                    Success = true,
                    Message = "Product registered successfully!",
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
                return BadRequest(new ResultViewModel
                {
                    Success = false,
                    Message = $"An error occurred while registering the product",
                    Data = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        /// <summary>
        /// Retrieves all products from the system.
        /// </summary>
        /// <remarks>This method returns a list of all available products wrapped in a result view model. 
        /// If the operation is successful, the response contains a success message and the product data. In case of an
        /// error, the response includes an error message and the exception details.</remarks>
        /// <returns>An <see cref="ActionResult"/> containing a <see cref="ResultViewModel"/> with the operation result. If
        /// successful, the <c>Data</c> property contains the list of products. If an error occurs, the <c>Data</c>
        /// property contains the error details.</returns>
        [HttpGet("GetAllProducts")]
        public async Task<ActionResult> GetAllProducts()
        {
            try
            {
                var result = await _productAppService.GetAllProducts();

                return Ok(new ResultViewModel
                {
                    Success = true,
                    Message = "Produtos recuperados com sucesso.",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel
                {
                    Success = false,
                    Message = "An error occurred while retrieving products.",
                    Data = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        /// <summary>
        /// Updates the details of an existing product.
        /// </summary>
        /// <remarks>This method uses the <c>PUT</c> HTTP verb and expects the product details to be
        /// provided in the request body. Ensure that the <paramref name="idProduct"/> corresponds to an existing
        /// product.</remarks>
        /// <param name="idProduct">The unique identifier of the product to be updated.</param>
        /// <param name="productViewModel">An object containing the updated product details. This must include all required fields for the product.</param>
        /// <returns>An <see cref="ActionResult"/> indicating the result of the operation.  Returns a 200 OK response with a
        /// success message if the product is updated successfully.  Returns a 404 Not Found response if the product
        /// does not exist or the update fails.  Returns a 500 Internal Server Error response if an unexpected error
        /// occurs.</returns>
        [HttpPut("EditProduct")]
        public async Task<ActionResult> EditProduct(Guid idProduct, [FromBody] ProductViewModel productViewModel)
        {
            try
            {
                var result = await _productAppService.UpdateProduct(idProduct, productViewModel);

                if(result)
                {
                     return Ok(new ResultViewModel
                    {
                        Success = true,
                        Message = "Product updated successfully!"
                    });
                }
                return NotFound(new ResultViewModel
                {
                    Success = false,
                    Message = "Product not found or update failed."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel
                {
                    Success = false,
                    Message = "An error occurred while updating the product.",
                    Data = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        /// <summary>
        /// Deletes a product identified by the specified ID.
        /// </summary>
        /// <remarks>This method performs a delete operation on the product resource. Ensure the <paramref
        /// name="idProduct"/>  corresponds to an existing product before calling this method.</remarks>
        /// <param name="idProduct">The unique identifier of the product to delete. Must be a valid <see cref="Guid"/>.</param>
        /// <returns>An <see cref="ActionResult"/> indicating the result of the operation.  Returns a 200 OK response with a
        /// success message if the product is deleted successfully.  Returns a 404 Not Found response if the product is
        /// not found or the deletion fails.  Returns a 500 Internal Server Error response if an unexpected error
        /// occurs.</returns>
        [HttpDelete("DeleteProduct")]
        public async Task<ActionResult> DeleteProduct(Guid idProduct)
        {
            try
            {
                var result = await _productAppService.DeleteProductAsync(idProduct);

                if(result)
                {
                    return Ok(new ResultViewModel
                    {
                        Success = true,
                        Message = "Product deleted successfully!"
                    });
                }
                return NotFound(new ResultViewModel
                {
                    Success = false,
                    Message = "Product not found or deletion failed."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel
                {
                    Success = false,
                    Message = "An error occurred while deleting the product.",
                    Data = ex.InnerException?.Message ?? ex.Message
                });
            }
        }
    }
}
