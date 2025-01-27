using System.Text.Json;

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

                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                    // Log the exception
                    logger.LogError("An unexpected error occurred: {Path}", context.Request.Path);

                    // Serialize the error response to JSON
                    var errorResponse = new
                    {
                        error = "An unexpected error occurred.",
                        statusCode = context.Response.StatusCode
                    };

                    var responseText = JsonSerializer.Serialize(errorResponse);

                    await context.Response.WriteAsync(responseText);
                });
            });

            app.UseMiddleware<Middlewares.LoggingMiddleware>();

            app.UseAuthorization();
            app.MapControllers();

            return app;
        }
    }
}
