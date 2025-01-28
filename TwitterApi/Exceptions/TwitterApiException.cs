using System;
using System.Net;

namespace TwitterApi.Exceptions
{
    public class TwitterApiException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public string? RawResponse { get; }

        public TwitterApiException(string message, HttpStatusCode statusCode, string? rawResponse = null) : base(message)
        {
            StatusCode = statusCode;
            RawResponse = rawResponse;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, StatusCode: {StatusCode}, RawResponse: {RawResponse}";
        }
    }
}
