using Microsoft.AspNetCore.Mvc;
using TwitterApi.Services;
using TwitterApi.DTOs;

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

        [HttpGet("tweets")]
        public async Task<IActionResult> GetTweets([FromQuery] TweetRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tweetDtos = await _twitterService.GetTweetsAsync(request.Query, request.MaxResults);
            return Ok(tweetDtos);
        }
    }
}
