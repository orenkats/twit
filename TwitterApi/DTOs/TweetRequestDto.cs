using System.ComponentModel.DataAnnotations;

namespace TwitterApi.DTOs
{
    public class TweetRequestDto
    {
        [Required(ErrorMessage = "Query parameter cannot be empty.")]
        public string Query { get; set; } = "news"; 

        [Range(1, 100, ErrorMessage = "MaxResults must be between 1 and 100.")]
        public int MaxResults { get; set; } = 10; 
    }
}
