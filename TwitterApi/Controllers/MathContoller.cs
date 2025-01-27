using Microsoft.AspNetCore.Mvc;
using TwitterApi.DTOs;
using TwitterApi.Services;

namespace TwitterApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MathController : ControllerBase
    {
        
        [HttpPost("multiply")]
        public IActionResult Multiply([FromBody] MultiplyRequest request)
        {
            if (request == null)
                return BadRequest("Invalid input. Please provide 'num1' and 'num2'.");

            var result = request.Num1 * request.Num2;
            return Ok(new { result });
        }
    }
}
