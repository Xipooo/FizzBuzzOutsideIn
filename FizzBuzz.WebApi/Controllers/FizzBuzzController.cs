using FizzBuzz.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FizzBuzz.WebApi.Controllers
{
    [ApiController]
    [Route("api/fizzbuzz")]
    public class FizzBuzzController : ControllerBase
    {
        private IFizzBuzzService _fizzBuzzService;

        public FizzBuzzController(IFizzBuzzService fizzBuzzService)
        {
            _fizzBuzzService = fizzBuzzService;
        }

        [HttpGet("{number}")]
        public IActionResult Get(int number)
        {
            var result = _fizzBuzzService.GetAnswer(number);
            if (result == "Invalid") return BadRequest();
            return Ok(result);
        }

        [HttpGet]
        public IActionResult Get(){
            return NotFound("Error: A parameter is required.");
        }
    }
}