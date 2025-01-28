using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using TwitterApi.Utils;

namespace TwitterApi.Extensions
{
    public static class ApplicationPipelineExtensions
    {
        public static WebApplication ConfigureApplicationPipeline(this WebApplication app)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                    if (exception == null) return;

                    var (statusCode, message) = ExceptionResponseMapper.MapToErrorResponse(exception);
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = statusCode;

                    var errorResponse = new { error = message, statusCode };
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
