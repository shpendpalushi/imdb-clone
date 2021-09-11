using System.Threading.Tasks;
using AutoMapper;
using IMDBClone.Data.Entities;
using IMDBClone.Data.Seed;
using IMDBClone.Domain.Definitions;
using IMDBClone.Domain.DTO.User;
using IMDBClone.Domain.Extensions.Types;
using IMDBClone.Domain.Service.Contracts;
using Microsoft.AspNetCore.Identity;

namespace IMDBClone.Domain.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IDataService _dataService;

        public UserService(UserManager<ApplicationUser> userManager, IMapper mapper,
            ITokenService tokenService, IDataService dataService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;
            _dataService = dataService;
        }
        public async Task<Result<UserDTO>> AddUserAsync(RegisterDTO model)
        {
            var user = _mapper.Map<RegisterDTO, ApplicationUser>(model);
            var registerResult = await _userManager.CreateAsync(user, model.Password);
            if (!registerResult.Succeeded) return Result.Fail<UserDTO>(registerResult.Errors.ToEnumerableString());
            var addToRole = await _userManager.AddToRoleAsync(user, RoleDefaults.User);
            if (!addToRole.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                return Result.Fail<UserDTO>(addToRole.Errors.ToEnumerableString());
            }
            var token = await _tokenService.CreateToken(user);
            UserDTO userDto = new ()
            {
                UserName = model.Email,
                Token = token
            };
            return Result.Ok(userDto);
        }

        public async Task<Result<UserDTO>> SignInAsync(LoginDTO loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Username);
            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password)) return Result.Fail<UserDTO>("Invalid credentials");
            var token = await _tokenService.CreateToken(user);
            var userDto = new UserDTO()
            {
                UserName = loginDto.Username,
                Token = token
            };
            return Result.Ok(userDto);
        }

        public async Task<ApplicationUserDTO> GetUserByEmailAsync(string email)
        {
            ApplicationUser user =
                await _dataService.FirstOrDefaultAsNoTrackingAsync<ApplicationUser>(
                    whereExpression: t => t.Email == email);
            return _mapper.Map<ApplicationUserDTO>(user);
        }
    }
}