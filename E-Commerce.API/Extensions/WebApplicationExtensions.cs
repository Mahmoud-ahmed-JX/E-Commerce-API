using Domain.Contracts;
using E_Commerce.API.Middlewares;

namespace E_Commerce.API.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> SeedDataAsync(this WebApplication app)
        {
           using var scope = app.Services.CreateScope();
            var ObjectSeeding = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await ObjectSeeding.SeedDataAsync();
            return app;
        }

        public static WebApplication AddExceptionHandlingMiddleware(this WebApplication app)
        {
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

            return app;
        }

        public static WebApplication UseSwaggerMiddleware(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurant API v1");
                options.RoutePrefix = string.Empty; // Optional: makes Swagger the home page
            });

            return app;
        }
    }
}
