using System.Text.Json;
using TwitterApi.Exceptions;
using Microsoft.AspNetCore.WebUtilities;
using TwitterApi.Models;
using TwitterApi.DTOs;

namespace TwitterApi.Services
{
    public class TwitterService : ITwitterService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<TwitterService> _logger;
        private readonly string _baseUrl;

        public TwitterService(HttpClient httpClient, ILogger<TwitterService> logger, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _logger = logger;
            _baseUrl = configuration["Twitter:BaseUrl"] ;
        }

        public async Task<IEnumerable<TweetDto>> GetTweetsAsync(string query = "news", int maxResults = 10)
        {
            var url = $"{_baseUrl}/tweets/search/recent?query={Uri.EscapeDataString(query)}&max_results={maxResults}";

            _logger.LogInformation("Sending request to Twitter API: {Url}", url);

            var tweetsResponse = await FetchFromApiAsync<TweetsResponse>(url);

            if (tweetsResponse.Data == null || !tweetsResponse.Data.Any())
            {
                _logger.LogWarning("No tweets found for query: {Query}", query);
                return Enumerable.Empty<TweetDto>();
            }

            var tweetDtos = tweetsResponse.Data.Select(t => new TweetDto
            {
                Text = t.Text
            }).ToList();

            _logger.LogInformation("Fetched and transformed {Count} tweets.", tweetDtos.Count);

            return tweetDtos;
        }

        private async Task<T> FetchFromApiAsync<T>(string url)
        {
            
            var response = await _httpClient.GetAsync(url);

            var rawResponse = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new TwitterApiException("Failed to fetch data.", response.StatusCode, rawResponse);
            }

            _logger.LogInformation("Successfully fetched data from {Url}", url);

            var result = JsonSerializer.Deserialize<T>(rawResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result;
        }

    }
}
