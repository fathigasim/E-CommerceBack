using EcommerceApplication.Features.ReqResService.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReqResController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReqResController(IMediator mediator)
        {
         _mediator = mediator;   
        }
        [HttpGet]
        public async Task< IActionResult> GetAsync(int Id) {

          var result=  await _mediator.Send(new ReqResUserQuery(Id));
          

              return  result.IsSuccess ? Ok(result) : BadRequest(result.ErrorMessage);       
        }
    }
}
