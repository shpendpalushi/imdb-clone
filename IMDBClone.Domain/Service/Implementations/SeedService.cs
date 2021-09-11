using System;
using System.Threading.Tasks;
using IMDBClone.Data.Entities;
using IMDBClone.Data.Seed;
using IMDBClone.Domain.Service.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IMDBClone.Domain.Service.Implementations
{
    public class SeedService
    {
        public static async void EnsureSeedData(IServiceProvider services)
        {
            using IServiceScope scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            
            using RoleManager<IdentityRole<Guid>> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            if (!await roleManager.Roles.AnyAsync())
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(RoleDefaults.Admin));
                await roleManager.CreateAsync(new IdentityRole<Guid>(RoleDefaults.User));
                
            }

            using UserManager<ApplicationUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            if ((await userManager.FindByNameAsync(UserDefaults.AdminUserName)) is null)
            {
                ApplicationUser adminUser = new()
                {
                    UserName = UserDefaults.AdminUserName,
                    Email = UserDefaults.AdminUserName,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(adminUser, UserDefaults.Password);
                await userManager.AddToRoleAsync(adminUser, RoleDefaults.Admin);
            }

            if ((await userManager.FindByNameAsync(UserDefaults.AdminUserName)) is null)
            {
                ApplicationUser backgroundUser = new()
                {
                    UserName = UserDefaults.UserUserName,
                    Email = UserDefaults.UserUserName,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(backgroundUser, UserDefaults.Password);
                await userManager.AddToRoleAsync(backgroundUser, RoleDefaults.User);
            }
        }
    }
}