using Microsoft.AspNetCore.Mvc;

namespace FizzBuzz.WebApi.Controllers
{
    [ApiController]
    [Route("api/fizzbuzz")]
    public class FizzBuzz : ControllerBase
    {
        [HttpGet("{number}")]
        public IActionResult Get(int number){
            return Ok();
        }
    }
}