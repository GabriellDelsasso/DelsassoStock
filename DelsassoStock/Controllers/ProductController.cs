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
        /// <remarks>
        /// This method processes the product registration request by delegating to the
        /// application service. Ensure that <paramref name="productViewModel"/> contains all required fields and valid
        /// data.
        /// </remarks>
        /// <param name="productViewModel">The product details to register. Must contain valid data for the product.</param>
        /// <returns>
        /// An <see cref="ActionResult"/> indicating the result of the operation.
        /// Returns  <see cref="BadRequestObjectResult"/> if the product data is invalid, or <see cref="OkObjectResult"/> containing
        /// the registered product details if the operation succeeds.
        /// </returns>
        [HttpPost("RegisterProduct")]
        public async Task<ActionResult> RegisterProduct(ProductViewModel productViewModel)
        {
            var result = await _productAppService.RegisterProduct(productViewModel);

            if (result == null)
                return BadRequest("Invalid product data.");

            return Ok(result);
        }

        /// <summary>
        /// Retrieves all available products.
        /// </summary>
        /// <remarks>
        /// This method returns a list of products from the underlying service. If no products
        /// are found, the method responds with a bad request status and an error message. Otherwise, it returns an HTTP
        /// 200 status with the list of products.
        /// </remarks>
        /// <returns>
        /// An <see cref="ActionResult"/> containing either a list of products with an HTTP 200 status or an error
        /// message with an HTTP 400 status if no products are found.
        /// </returns>
        [HttpGet("GetAllProducts")]
        public async Task<ActionResult> GetAllProducts()
        {
            var result = await _productAppService.GetAllProducts();

            if (result.Count() == 0)
                return BadRequest("Failure to search for products!");

            return Ok(result);
        }

        /// <summary>
        /// Updates the details of an existing product.
        /// </summary>
        /// <remarks>
        /// This method requires a valid <paramref name="idProduct"/> and a properly populated 
        /// <paramref name="productViewModel"/> object. Ensure that the product exists before calling this
        /// method.
        /// </remarks>
        /// <param name="idProduct">The unique identifier of the product to be updated.</param>
        /// <param name="productViewModel">The updated product details provided in the request body.</param>
        /// <returns>
        /// An <see cref="ActionResult"/> indicating the result of the operation.  Returns <see langword="Ok"/> if the
        /// product was updated successfully;  otherwise, returns <see langword="BadRequest"/> with an error message.
        /// </returns>
        [HttpPut("EditProduct")]
        public async Task<ActionResult> EditProduct(Guid idProduct, [FromBody] ProductViewModel productViewModel)
        {
            var result = await _productAppService.UpdateProduct(idProduct, productViewModel);

            if (!result)
                return BadRequest("Failed to update product");

            return Ok("Product updated successfully!");
        }

        /// <summary>
        /// Deletes a product identified by the specified ID.
        /// </summary>
        /// <remarks>
        /// This method performs an asynchronous operation to delete a product. Ensure that the
        /// <paramref name="idProduct"/> corresponds to an existing product.
        /// </remarks>
        /// <param name="idProduct">The unique identifier of the product to delete.</param>
        /// <returns>
        /// An <see cref="ActionResult"/> indicating the result of the operation.  Returns <see
        /// cref="BadRequestResult"/> if the deletion fails, or <see cref="OkResult"/> if the product is successfully
        /// deleted.
        /// </returns>
        [HttpDelete("DeleteProduct")]
        public async Task<ActionResult> DeleteProduct(Guid idProduct)
        {
            var result = await _productAppService.DeleteProductAsync(idProduct);

            if (!result)
                return BadRequest("Failed to delete product");

            return Ok("Product deleted successfully!");
        }
    }
}
