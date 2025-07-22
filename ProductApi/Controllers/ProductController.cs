using Microsoft.AspNetCore.Mvc;
using ProductApi.ProductServices;
using Shared;

namespace ProductApi.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(
            ILogger<ProductController> logger,
            IProductService productService
        )
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            _logger.LogInformation("Add product");
            await _productService.AddProduct(product);
            return Created();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            _logger.LogInformation($"Delete product {id}");
            await _productService.DeleteProduct(id);
            return NoContent();
        }
    }
}