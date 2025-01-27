using TwitterApi.DTOs;

namespace TwitterApi.Services
{
    public interface ITwitterService
    {
        Task<IEnumerable<TweetDto>> GetLast10TweetsAsync();
    }
}
