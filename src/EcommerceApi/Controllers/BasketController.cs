using EcommerceApplication.Features.Basket.Commands.AddToBasket;
using EcommerceApplication.Features.Basket.Commands.ClearBasket;
using EcommerceApplication.Features.Basket.Commands.RemoveFromBasket;
using EcommerceApplication.Features.Basket.DTOs;
using MediaRTutorialApplication.Features.Basket.DTOs;
using MediaRTutorialApplication.Features.Basket.Queries.GetBasket;
using MediaRTutorialApplication.Features.Basket.Queries.GetBasketSummary;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediaRTutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BasketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetBasket")]
        public async Task<ActionResult<BasketDto>> GetBasket()
        {
            var result = await _mediator.Send(new GetBasketQuery());
            if (result.IsSuccess)
            {
                if (result.Data.Items.Any())
                {
                    return Ok(result.Data);
                }
                else
                {
                    return NotFound( new { message= "No items found in the basket" });
                }
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
            
        }

        [HttpGet("Summary")]
        public async Task<ActionResult<BasketSummaryDto>> GetBasketSummary()
        {
            var result = await _mediator.Send(new GetBasketSummaryQuery());
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpPost("Add")]
        public async Task<ActionResult<BasketDto>> AddToBasket([FromBody] AddToBasketCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess 
                ?
                Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpPost("Remove")]
        public async Task<ActionResult<BasketDto>> RemoveFromBasket(RemoveFromBasketCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpPost("Clear")]
        public async Task<ActionResult> ClearBasket()
        {
            var result = await _mediator.Send(new ClearBasketCommand());
            return result.IsSuccess ? Ok() : BadRequest(result.ErrorMessage);
        }
    }
}
