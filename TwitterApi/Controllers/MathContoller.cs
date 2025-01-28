using Microsoft.AspNetCore.Mvc;
using TwitterApi.DTOs;

namespace TwitterApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MathController : ControllerBase
    {
        [HttpPost("multiply")]
        public IActionResult Multiply([FromBody] MultiplyRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = request.Num1* request.Num2;

            return Ok(new { result });
        }
    }
}
