using MediaRTutorialApplication.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceInfrastructure.Extensions
{
    public static class HostExtensions
    {
        public static async Task<IHost> SeedDatabaseAsync(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var seeder = services.GetRequiredService<IDbSeeder>();
                await seeder.SeedAsync();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<IDbSeeder>>();
                logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }

            return host;
        }
    }
}
