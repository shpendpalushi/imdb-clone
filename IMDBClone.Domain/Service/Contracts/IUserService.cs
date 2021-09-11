using System.Threading.Tasks;
using IMDBClone.Domain.Definitions;
using IMDBClone.Domain.DTO.User;

namespace IMDBClone.Domain.Service.Contracts
{
    public interface IUserService
    {
        Task<Result<UserDTO>> AddUserAsync(RegisterDTO registerDto);
        Task<Result<UserDTO>> SignInAsync(LoginDTO loginDto);
        Task<ApplicationUserDTO> GetUserByEmailAsync(string email);
    }
}