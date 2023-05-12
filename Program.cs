using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using ecommerce_website_simple.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_website_simple
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            //Create database if it doesnot exist
            CreateDbIfNotExistsAsync(host);

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        private static async Task CreateDbIfNotExistsAsync(IHost host)
        {
            // Get the scope from the service provider
            using (var scope = host.Services.CreateScope())
            {
                //Gets the service provider from the scope
                var services = scope.ServiceProvider;

                try
                {
                    await DBinitializer.InitializeAsync(services);
                }
                catch (Exception ex)
                {
                    // Gets the logger service
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    // Logs the error
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }
    }
}
