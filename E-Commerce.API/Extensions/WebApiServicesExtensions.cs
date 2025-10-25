using E_Commerce.API.Factories;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Extensions
{
    public static class WebApiServicesExtensions
    {
        public static IServiceCollection AddWebApiServices(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddOpenApi();
            services.AddSwaggerGen(C =>
            {
                C.SwaggerDoc("v1", new() { Title = "E-Commerce API", Version = "v1" });
            });


            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationErrorResponse;
            });

            return services;
        }
    }
}
