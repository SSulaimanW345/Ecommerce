using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_website_simple.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace ecommerce_website_simple.Data
{
    public static class DBinitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            
            string[] roleNames = {"Admin", "User"};
            IdentityResult roleResult;
            //Adding two roles to database, can add more roles 
            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            await dbContext.SaveChangesAsync();
            //Creating three product categories 
            var categoryLaptop = new ProductCategory
            {
                CategoryID = 1,
                CategoryName = "Laptops",
                CategoryDescription = "High-performance laptops for gaming and productivity"
            };

            var categorySmartphones = new ProductCategory
            {
                CategoryID = 2,
                CategoryName = "Smartphones",
                CategoryDescription = "Powerful smartphones with high-quality cameras"
            };

            var categoryAudio = new ProductCategory
            {
                CategoryID = 3,
                CategoryName = "Audio",
                CategoryDescription = "Headphones, speakers, and other audio equipment"
            };

            //Creating 3 products for testing 
            //Note: Image urls same for all while testing
            dbContext.ProductCategories.AddRange(categoryLaptop, categoryAudio, categorySmartphones);
            await dbContext.SaveChangesAsync();
            var products = new List<Product>
            {
                new Product { ProductName = "Laptop", Description = "A high-performance laptop for gaming and productivity", ProductPrice = 999.99, StockQuantity = 50, ProductImgURL = "/images/LaptopImage.jfif", Category = categoryLaptop },
                new Product { ProductName = "Smartphone", Description = "A powerful smartphone with a high-quality camera", ProductPrice = 599.99, StockQuantity = 100, ProductImgURL = "/images/LaptopImage.jfif", Category = categorySmartphones },
                new Product { ProductName = "Headphones", Description = "Noise-cancelling headphones for an immersive audio experience", ProductPrice = 249.99, StockQuantity = 25, ProductImgURL = "/images/LaptopImage.jfif", Category = categoryAudio },
                //new Product { productName = "Smartwatch", Description = "A stylish smartwatch with fitness tracking and GPS capabilities", productPrice = 199.99, stockQuantity = 75, productImgURL = "https://example.com/smartwatch.jpg", category = 4 },
                //new Product { productName = "TV", Description = "A high-resolution TV with smart features and voice control", productPrice = 1499.99, stockQuantity = 10, productImgURL = "https://example.com/tv.jpg", category = 5 },
                //new Product { productName = "Wireless Speakers", Description = "Wireless speakers with 360-degree sound for an immersive audio experience", productPrice = 349.99, stockQuantity = 40, productImgURL = "https://example.com/wireless-speakers.jpg", CategoryID = 3 },
                //new Product { productName = "Tablet", Description = "A portable tablet with a high-resolution display and long battery life", productPrice = 499.99, stockQuantity = 60, productImgURL = "https://example.com/tablet.jpg", category = 1 },
                //new Product { productName = "Camera", Description = "A professional-grade camera with advanced features for photography and videography", productPrice = 1499.99, stockQuantity = 15, productImgURL = "https://example.com/camera.jpg", category = 2 },
                //new Product { productName = "Gaming Chair", Description = "A comfortable gaming chair with ergonomic design and lumbar support", productPrice = 299.99, stockQuantity = 20, productImgURL = "https://example.com/gaming-chair.jpg", category = 6 },
                //new Product { productName = "Gaming Mouse", Description = "A high-performance gaming mouse with customizable RGB lighting", productPrice = 79.99, stockQuantity = 50, productImgURL = "https://example.com/gaming-mouse.jpg", category = 6 },
            };
            dbContext.AddRange(products);
            await dbContext.SaveChangesAsync();
            
            // Adding Two Users: 1 in USER role and 2 in ADMIN role
            //You can only use this ADMIN user to log into ADMIN panel
            //The credentials are:
            //username:exampleAdmin@gmail.com
            //password:admin123

            ApplicationUser applicationAdmin = await UserManager.FindByEmailAsync("exampleAdmin@gmail.com");

            if (applicationAdmin == null)
            {
                applicationAdmin = new ApplicationUser()
                {
                    UserName = "oiawd",
                    Email = "exampleAdmin@gmail.com",
                    FirstName = "oi",
                    LastName = "awd",
                };
                var result = await UserManager.CreateAsync(applicationAdmin, "admin123");
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(applicationAdmin, "Admin");
                    await dbContext.SaveChangesAsync();
                }
            }
            
            
            ApplicationUser defaultUser = await UserManager.FindByEmailAsync("exampleUser@gmail.com");

            if (defaultUser == null)
            {
                defaultUser = new ApplicationUser()
                {
                    UserName = "first",
                    Email = "exampleUser@gmail.com",
                    FirstName = "first",
                    LastName = "user",
                };
                var result = await UserManager.CreateAsync(defaultUser, "example123");
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(defaultUser, "User");
                    await dbContext.SaveChangesAsync();
                }
            }
            await dbContext.SaveChangesAsync();
        }
    }
}