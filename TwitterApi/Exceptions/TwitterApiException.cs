using System;
using System.Net;

namespace TwitterApi.Exceptions
{
    public class TwitterApiException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public TwitterApiException(string message, HttpStatusCode statusCode) 
            : base(message)
        {
            StatusCode = statusCode;
        }

        public TwitterApiException(string message, HttpStatusCode statusCode, Exception innerException) 
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, StatusCode: {StatusCode}";
        }
    }
}
