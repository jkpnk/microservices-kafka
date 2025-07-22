using Microsoft.AspNetCore.Mvc;
using OrderApi.OrderServices;
using Shared;

namespace OrderApi.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;

        public OrderController(
            ILogger<OrderController> logger,
            IOrderService orderService
        )
        {
            _logger = logger;
            _orderService = orderService;
        }

        [HttpGet("start-consuming-service")]
        public async Task<IActionResult> StartService()
        {
            await _orderService.StartConsumingService();
            return NoContent();
        }

        [HttpGet("get-product")]
        public IActionResult GetProduct()
        {
            var product = _orderService.GetProducts();
            return Ok(product);
        }

        [HttpPost("add-order")]
        public IActionResult AddOrder(Order order)
        {
            _orderService.AddOrder(order);
            return Ok("Order placed");
        }

        [HttpGet("order-summary")]
        public IActionResult GetOrdersSummary() => Ok(_orderService.GetOrdersSummary());
    }
}