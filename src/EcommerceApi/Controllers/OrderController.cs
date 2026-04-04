
using EcommerceApplication.Features.Orders.Commands;
using EcommerceApplication.Features.Orders.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

namespace MediaRTutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IMediator _mediator;

        public OrderController(ILogger<OrderController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        [HttpGet("GetOrders")]
        public async Task <IActionResult> GetOrders(string? q,int pageNumber,int pageSize)
        {
            // Placeholder for getting orders
          var result=await  _mediator.Send(new GetAllOrdersQuery(q,pageNumber,pageSize));
            return Ok(result);
        }

        [HttpGet("GetOrderById/{id}")] 
        public IActionResult GetOrderById(Guid id)
        {
            // Placeholder for getting a specific order by ID
            return Ok(new { Message = $"Get order with ID: {id}" });
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand createOrderCommand)
        {
            if (createOrderCommand == null)
                return BadRequest(new { Message = "Invalid order data" });

            try
            {
                var result = await _mediator.Send(createOrderCommand); // ✅ awaited

                if (!result.IsSuccess)
                    return BadRequest(new { Message = result.ErrorMessage });

                return Ok(new { Message = "Order created successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
