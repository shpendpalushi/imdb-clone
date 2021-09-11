using System.Threading.Tasks;
using IMDBClone.Domain.Extensions.HostExtensions;
using IMDBClone.Domain.Service.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace IMDBClone.Application
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            SeedService.EnsureSeedData(host.Services);
            await host.AddAutomaticMigrationExtension();
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}