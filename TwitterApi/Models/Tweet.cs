namespace TwitterApi.Models
{
    public class Tweet
    {
        public string? Text { get; set; } 
        public string? Id { get; set; } 
        public List<string>? EditHistoryTweetIds { get; set; } 

    }

}
