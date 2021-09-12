using IMDBClone.Domain.Service.Contracts;
using IMDBClone.Domain.Service.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace IMDBClone.Domain.Extensions.ServiceExtensions
{
    public static class RepositoryExtensions
    {
        public static void AddDataAccessServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDataService, DataService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IActorService, ActorService>();
        }
        
    }
}