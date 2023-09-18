﻿using EcommerceAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
                    var json = File.ReadAllText("Data/UsersSeed.json");
                    var seedData = JsonConvert.DeserializeObject<SeedModel>(json);

                    foreach (var userData in seedData.Users)
                    {
                        userManager.CreateAsync(userData, userData.Password).Wait();
                    }
                }

                if (!context.Products.Any())
                {
                    var jsonProducts = File.ReadAllText("Data/ProductsSeed.json");
                    var seedProducts = JsonConvert.DeserializeObject<SeedModel>(jsonProducts);

                    foreach (var productData in seedProducts.Products)
                    {
                        var user = context.Users.FirstOrDefault(u => u.Email == productData.UserEmail);

                        if (user != null)
                        {
                            var product = new Product
                            {
                                Name = productData.Name,
                                Description = productData.Description,
                                Price = productData.Price,
                                Quantity = productData.Quantity,
                                CreatedDate = productData.CreatedDate,
                                IsAvailable = productData.IsAvailable,
                                UserId = user.Id,
                                UserEmail = productData.UserEmail
                            };

                            context.Products.Add(product);
                        }
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}