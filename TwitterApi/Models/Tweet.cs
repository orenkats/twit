namespace TwitterApi.Models
{
    public class Tweet
    {
        public string? Text { get; set; } 
        public string? Id { get; set; } 
        public List<string>? EditHistoryTweetIds { get; set; } 

    }
    public class TweetsResponse
    {
        public List<Tweet>? Data { get; set; } 
        public Meta? Meta { get; set; } 
    }

    public class Meta
    {
        public string? NewestId { get; set; } 
        public string? OldestId { get; set; } 
        public int? ResultCount { get; set; } 
        public string? NextToken { get; set; } 
    }
}
