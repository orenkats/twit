using Microsoft.AspNetCore.Mvc;
using TwitterApi.Services;

namespace TwitterApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TwitterController : ControllerBase
    {
        private readonly ITwitterService _twitterService;

        public TwitterController(ITwitterService twitterService)
        {
            _twitterService = twitterService;
        }

        [HttpGet("last10tweets")]
        public async Task<IActionResult> GetLast10Tweets()
        {
            var tweetDtos = await _twitterService.GetLast10TweetsAsync();
            return Ok(tweetDtos);
        }
    }
}
