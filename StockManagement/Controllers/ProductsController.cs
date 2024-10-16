using Microsoft.AspNetCore.Mvc;
using StockManagement.Common.Domain.Interfaces;
using StockManagement.Common.Domain.Models.ViewModel;

namespace StockManagement.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController(IProductRepository productRepository) : ControllerBase
    {
        private readonly IProductRepository _productRepository = productRepository;

        [HttpGet]
        public async Task<ActionResult<ProductViewModel>> Get([FromQuery] GetProductViewModel model)
        {
            return Ok(await _productRepository.GetProductViewModel(model));
        }
        [HttpPost]
        public async Task<ActionResult<ProductViewModel>> Post([FromBody] PostProductViewModel model)
        {
            try
            {
                return Ok(await _productRepository.PostProductViewModel(model));
            }
            catch (Exception ex)
            {
                return BadRequest(new {ex.Message });
            }
        }
    }
}
