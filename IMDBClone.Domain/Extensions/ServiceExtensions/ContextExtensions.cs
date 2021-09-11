using Microsoft.Extensions.DependencyInjection;
using IMDBClone.Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IMDBClone.Domain.Extensions.ServiceExtensions
{
    public static class ContextExtensions
    {
        public static void AddExtendedDatabaseContextService(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(opt =>
            opt.UseNpgsql(connectionString, x => x.MigrationsAssembly("IMDBClone.Data")));
        }
    }
}