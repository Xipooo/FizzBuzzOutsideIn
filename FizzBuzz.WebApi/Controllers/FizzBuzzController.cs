using System.Threading.Tasks;
using FizzBuzz.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;

namespace FizzBuzz.WebApi.Controllers
{
    [ApiController]
    [Route("api/fizzbuzz")]
    public class FizzBuzzController : ControllerBase
    {
        private IFizzBuzzService _fizzBuzzService;
        private IFeatureManager _featureManager;

        public FizzBuzzController(IFizzBuzzService fizzBuzzService, IFeatureManager featureManager)
        {
            _fizzBuzzService = fizzBuzzService;
            _featureManager = featureManager;
        }

        [HttpGet("{number}")]
        public async Task<IActionResult> Get(int number)
        {
            if (!await _featureManager.IsEnabledAsync(FeatureFlags.FizzBuzz)) return NotFound();
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