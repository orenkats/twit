using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using TwitterApi.Exceptions;

namespace TwitterApi.Extensions
{
    public static class ApplicationPipelineExtensions
    {
        public static WebApplication ConfigureApplicationPipeline(this WebApplication app)
        {
            // Add built-in exception handling middleware
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
                    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                    context.Response.ContentType = "application/json";

                    // Determine the appropriate status code
                    if (exception is TwitterApiException twitterEx)
                    {
                        context.Response.StatusCode = (int)twitterEx.StatusCode;
                    }
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    }

                    // Log the exception
                    logger.LogError(exception, "An error occurred during request processing.");

                    // Build and serialize the error response
                    var errorResponse = new
                    {
                        error = exception?.Message ?? "An unexpected error occurred.",
                        statusCode = context.Response.StatusCode
                    };

                    await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
                });
            });

            app.UseMiddleware<Middlewares.RequestResponseLoggingMiddleware>();

            app.UseAuthorization();
            app.MapControllers();

            return app;
        }
    }
}
