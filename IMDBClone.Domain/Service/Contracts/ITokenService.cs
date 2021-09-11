using System.Threading.Tasks;
using IMDBClone.Data.Entities;
using Microsoft.Extensions.Configuration;

namespace IMDBClone.Domain.Service.Contracts
{
    public interface ITokenService
    {
        Task<string> CreateToken(ApplicationUser user);
    }
}