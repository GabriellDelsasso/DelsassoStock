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

        [HttpPost("CreateSale")]
        public async Task<ActionResult> CreateSale([FromBody] SaleViewModel saleViewModel)
        {
            var result = await _saleAppService.CreateSale(saleViewModel);

            if(!result)
                return BadRequest("Failed to create sale. Please check the provided data.");

            return Ok("Sale created successfully.");
        }
    }
}
