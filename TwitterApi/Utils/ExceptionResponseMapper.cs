using TwitterApi.Exceptions;

namespace TwitterApi.Utils
{
    public static class ExceptionResponseMapper
    {
        public static (int StatusCode, string Message) MapToErrorResponse(Exception? exception)
        {
            if (exception == null)
            {
                return (StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }

            switch (exception)
            {
                case TwitterApiException twitterEx:
                    return ((int)twitterEx.StatusCode, twitterEx.Message);

                case ValidationException validationEx:
                    return (StatusCodes.Status400BadRequest, validationEx.Message);

                default:
                    return (StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }
    }
}
