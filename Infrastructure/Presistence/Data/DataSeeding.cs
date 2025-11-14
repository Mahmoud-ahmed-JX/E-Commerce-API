using Domain.Contracts;
using Domain.Entities.IdentityModule;
using Domain.Entities.ProductModule;
using Microsoft.AspNetCore.Identity;
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
    public class DataSeeding(StoreDbContext _dbContext,RoleManager<IdentityRole> _roleManager,UserManager<User> _userManager) : IDataSeeding
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

        public async Task SeedIdentityDataAsync()
        {
            // No identity data to seed
            if (!_roleManager.Roles.Any())
            {
                //Create Roles
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));



            }
            if (!_userManager.Users.Any())
            {
                var adminuser = new User
                {
                    DisplayName = "Admin",
                    UserName = "Admin",
                    Email = "Admin@gmail.com",
                    PhoneNumber = "01204321050"
                };
                var superAdminuser = new User
                {
                    DisplayName = "SuperAdmin",
                    UserName = "SuperAdmin",
                    Email = "superAdmin@gmail.com",
                    PhoneNumber = "01204321051",
                    };
                //Create Users
                await _userManager.CreateAsync(adminuser, "p@$$W0rd");
                await _userManager.CreateAsync(superAdminuser, "p@$$W0rd");

                //Add Roles to Users
                await _userManager.AddToRoleAsync(adminuser, "Admin");
                await _userManager.AddToRoleAsync(superAdminuser, "SuperAdmin");
            }
        }
    }
}
