using DelsassoStock.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DelsassoStock.Controllers
{
    [Route("Product")]
    public class ProductController : ControllerBase
    {

        [HttpPost("RegisterProduct")]
        public async Task<ActionResult> RegisterProduct([FromBody] ProductViewModel productViewModel)
        {

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
