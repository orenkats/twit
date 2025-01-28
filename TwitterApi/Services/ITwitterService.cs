using TwitterApi.DTOs;

namespace TwitterApi.Services
{
    public interface ITwitterService
    {
        Task<IEnumerable<TweetDto>> GetTweetsAsync(string query , int maxResults );
    }
}
