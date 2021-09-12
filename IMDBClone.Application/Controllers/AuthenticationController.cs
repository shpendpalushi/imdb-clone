using System.Threading.Tasks;
using IMDBClone.Application.Controllers.Base;
using IMDBClone.Domain.DTO.User;
using IMDBClone.Domain.Extensions.Types;
using IMDBClone.Domain.Service.Contracts;
using IMDBClone.Domain.Validations;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace IMDBClone.Application.Controllers
{
    [EnableCors("CorsApi")]
    public class AuthenticationController : BaseApiController
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("api/registration")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            var validator = new RegisterValidator(_userService);
            var validationResult = await validator.ValidateAsync(registerDto);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors.ToListString());
            
            var result = await _userService.AddUserAsync(registerDto);
            if (!result.Success) return BadRequest(result.Error);
            return Ok(result.Value);
        }

        [HttpPost("api/access")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var loginValidator = new LoginValidator();
            var validationResult = await loginValidator.ValidateAsync(loginDto);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors.ToListString());
            var result = await _userService.SignInAsync(loginDto);
            if (!result.Success) return BadRequest(result.Error);
            return Ok(result.Value);
        }
    }
}