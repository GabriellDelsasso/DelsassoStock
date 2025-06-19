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

        [HttpPost("RegisterProduct")]
        public async Task<ActionResult> RegisterProduct(ProductViewModel productViewModel)
        {
            var result = await _productAppService.RegisterProduct(productViewModel);

            if (result == null)
                return BadRequest("Invalid product data.");

            return Ok(result);
        }

        [HttpGet("GetAllProducts")]
        public async Task<ActionResult> GetAllProducts()
        {

        }

        [HttpPost("EditProduct")]
        public async Task<ActionResult> EditProduct(Guid id, [FromBody] ProductViewModel productViewModel)
        {

        }

        [HttpDelete("DeleteProduct")]
        public async Task<ActionResult> DeleteProduct(Guid id)
        {

        }
    }
}
