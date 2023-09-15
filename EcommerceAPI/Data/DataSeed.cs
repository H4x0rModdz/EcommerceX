using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EcommerceAPI.Models;
using Newtonsoft.Json;

namespace EcommerceAPI.Data
{
    public static class DataSeed
    {
        public static void SeedData(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                       serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (!context.Users.Any())
                {
                    var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
                    var json = File.ReadAllText("Data/SeedData.json");
                    var seedData = JsonConvert.DeserializeObject<SeedModel>(json);

                    foreach (var userData in seedData.Users)
                    {
                        userManager.CreateAsync(userData, userData.Password).Wait();
                    }
                }

                if (!context.Products.Any())
                {
                    var json = File.ReadAllText("Data/SeedData.json");
                    var seedData = JsonConvert.DeserializeObject<SeedModel>(json);

                    foreach (var productData in seedData.Products)
                    {
                        var product = new Product
                        {
                            Name = productData.Name,
                            Description = productData.Description,
                            Price = productData.Price,
                            CreatedDate = productData.CreatedDate,
                            IsAvailable = productData.IsAvailable,
                        };
                        context.Products.Add(product);
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}