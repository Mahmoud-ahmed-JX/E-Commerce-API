
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using Presistence.Repositories;
using Services;
using Services.Abstraction.Contracts;
using Services.Implementation;
using System.Threading.Tasks;

namespace E_Commerce.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddSwaggerGen(C =>
            {
                C.SwaggerDoc("v1", new() { Title = "E-Commerce API", Version = "v1" });
            });

            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddAutoMapper(cfg => { },typeof(AssemblyReference).Assembly); 

            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddScoped<IServiceManager, ServiceManager>();

            using var app = builder.Build();
            var scope= app.Services.CreateScope();
            var ObjectSeeding=scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await ObjectSeeding.SeedDataAsync();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurant API v1");
                    options.RoutePrefix = string.Empty; // Optional: makes Swagger the home page
                });
            {
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}
