using EcommerceApplication.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JsonPlacehodlerController : ControllerBase
    {
        private readonly IJsonPlacehodlerService _jsonPlacehodlerService;

        public JsonPlacehodlerController(IJsonPlacehodlerService jsonPlacehodlerService)
        {
            _jsonPlacehodlerService = jsonPlacehodlerService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string q, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            var result = await _jsonPlacehodlerService.GetPostsAsync(q, page, pageSize, cancellationToken);
            return Ok(result);
        }

        // GET api/<JsonPlacehodlerController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<JsonPlacehodlerController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<JsonPlacehodlerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<JsonPlacehodlerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
