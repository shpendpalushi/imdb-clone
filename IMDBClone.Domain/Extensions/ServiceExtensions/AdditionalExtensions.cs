using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using AutoMapper;
using IMDBClone.Data.Entities;
using IMDBClone.Data.Persistence;
using IMDBClone.Domain.Mapper;

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
        public static void AddAutoMapperConfig(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mapperConfig =>
            {
                mapperConfig.AddProfile(new ApplicationMapper());
                mapperConfig.AddProfile(new ApplicationUserMapper());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
