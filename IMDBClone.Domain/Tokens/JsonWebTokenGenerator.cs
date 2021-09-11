using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IMDBClone.Domain.DTO.User;

namespace IMDBClone.Domain.Tokens
{
    public class JsonWebTokenGenerator
    {
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<IdentityUser<Guid>> _userManager;

        public JsonWebTokenGenerator(IConfiguration config, UserManager<IdentityUser<Guid>> userManager)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("Bearer").GetValue<string>("Key"))); ;
            _userManager = userManager;
        }

        public string GenerateJsonWebToken(LoginDTO loginModel, IConfiguration config)
        {
            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, loginModel.Username),
                new Claim(JwtRegisteredClaimNames.Email, loginModel.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var jwtToken = new JwtSecurityToken(issuer: config.GetSection("Bearer").GetValue<string>("Issuer"), audience: config.GetSection("Bearer").GetValue<string>("Audience"),
                claims:claims, expires: DateTime.Now.AddMinutes(240),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
