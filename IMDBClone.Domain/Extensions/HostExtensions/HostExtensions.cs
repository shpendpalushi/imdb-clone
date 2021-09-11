using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using IMDBClone.Data.Entities;
using IMDBClone.Data.Persistence;

namespace IMDBClone.Domain.Extensions.HostExtensions
{
    public static class HostExtensions
    {
        public static async Task AddAutomaticMigrationExtension(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ApplicationDbContext>();
            services.GetRequiredService<UserManager<ApplicationUser>>();
            await context.Database.MigrateAsync();
        }
    }
}
