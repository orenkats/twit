using TwitterApi.Services;

namespace TwitterApi.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var bearerToken = configuration["Twitter:BearerToken"];
           
            services.AddHttpClient<ITwitterService, TwitterService>(client =>
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                    "Bearer",
                    bearerToken
                );
            });

            services.AddControllers();

            return services;
        }
    }
}
