using System.ComponentModel.DataAnnotations;

namespace TwitterApi.DTOs
{
    public class MultiplyRequest
    {
        [Required(ErrorMessage = "'Num1' is required.")]
        public double? Num1 { get; set; } 

        [Required(ErrorMessage = "'Num2' is required.")]
        public double? Num2 { get; set; }
    }
}
