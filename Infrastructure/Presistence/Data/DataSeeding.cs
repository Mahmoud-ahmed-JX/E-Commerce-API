using Domain.Contracts;
using Domain.Entities.ProductModule;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Presistence.Data
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
    {
        public  async Task SeedDataAsync()
        {

            try
            {
                var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
                if (pendingMigrations.Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }

                if (!_dbContext.ProductBrands.Any())
                {
                    var brandsData = File.OpenRead("..\\Infrastructure\\Presistence\\Data\\DataSeed\\brands.json");
                    var brands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(brandsData);
                    if (brands is not null && brands.Any())
                        await _dbContext.ProductBrands.AddRangeAsync(brands);
                }

                if (!_dbContext.ProductTypes.Any())
                {
                    var brandsTypes = File.OpenRead("..\\Infrastructure\\Presistence\\Data\\DataSeed\\types.json");
                    var types = await JsonSerializer.DeserializeAsync<List<ProductType>>(brandsTypes);
                    if (types is not null && types.Any())
                        await _dbContext.ProductTypes.AddRangeAsync(types);
                }

                if (!_dbContext.Products.Any())
                {
                    var productsData = File.OpenRead("..\\Infrastructure\\Presistence\\Data\\DataSeed\\products.json");
                    var products = await JsonSerializer.DeserializeAsync<List<Product>>(productsData);
                    if (products is not null && products.Any())
                        await _dbContext.Products.AddRangeAsync(products);
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
