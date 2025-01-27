using System.Text.Json;
using TwitterApi.Models;
using TwitterApi.DTOs;

namespace TwitterApi.Services
{
    public class TwitterService : ITwitterService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<TwitterService> _logger;

        public TwitterService(HttpClient httpClient, ILogger<TwitterService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<TweetDto>> GetLast10TweetsAsync()
        {
            var query = "news";
            var maxResults = 10;
            var url = $"https://api.twitter.com/2/tweets/search/recent?query={query}&max_results={maxResults}";

            _logger.LogInformation("Sending request to Twitter API: {Url}", url);

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Failed to fetch tweets. Status: {StatusCode}", response.StatusCode);
                return Enumerable.Empty<TweetDto>();
            }

            var rawResponse = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("Raw Twitter API Response: {Response}", rawResponse);

            var tweetsResponse = JsonSerializer.Deserialize<TweetsResponse>(rawResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (tweetsResponse?.Data == null || !tweetsResponse.Data.Any())
            {
                _logger.LogWarning("No tweets found for query: {Query}", query);
                return Enumerable.Empty<TweetDto>();
            }

            // Map the model to the DTO
            var tweetDtos = tweetsResponse.Data.Select(t => new TweetDto
            {
                Text = t.Text,
                
            }).ToList();

            _logger.LogInformation("Fetched and transformed {Count} tweets.", tweetDtos.Count);

            return tweetDtos;
        }
    }
}
