using Domain.Contracts;
using Presistence.Data;
using Services.Abstraction.Contracts;
using Services.Implementation;
using System.Reflection.Metadata;

namespace E_Commerce.API.Extensions
{
    public static class CoreServicesExtensions
    {

        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => { }, typeof(AssemblyReference).Assembly);

            services.AddScoped<IServiceManager, ServiceManager>();

            return services;
        }
    }
}
