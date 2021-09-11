using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using IMDBClone.Data.Entities;
using IMDBClone.Data.Persistence;

namespace IMDBClone.Domain.Extensions.ServiceExtensions
{
    public static class AdditionalExtensions
    {
        public static void AddAdditionalExtensions(this IServiceCollection services)
        {
            services.AddIdentityCore<ApplicationUser>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
            })
                .AddRoles<IdentityRole<Guid>>()
                .AddRoleManager<RoleManager<IdentityRole<Guid>>>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddRoleValidator<RoleValidator<IdentityRole<Guid>>>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
        }
    }
}
