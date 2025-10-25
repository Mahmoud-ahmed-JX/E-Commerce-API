
using Domain.Contracts;
using E_Commerce.API.Extensions;
using E_Commerce.API.Factories;
using E_Commerce.API.Middlewares;
using Microsoft.AspNetCore.Mvc;
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

            #region DI
            // Add services to the container.
            builder.Services.AddWebApiServices();

            builder.Services.AddInfrastructureServices(builder.Configuration);

            builder.Services.AddCoreServices();
            #endregion


            #region PipeLine - Middlewares
            using var app = builder.Build();
            await app.SeedDataAsync();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerMiddleware();

            }




            app.AddExceptionHandlingMiddleware();
            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseStaticFiles();

            app.MapControllers();

            app.Run(); 
            #endregion
        }
    }
}
