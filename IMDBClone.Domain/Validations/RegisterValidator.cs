using System.Threading.Tasks;
using FluentValidation;
using IMDBClone.Domain.DTO.User;
using IMDBClone.Domain.Service.Contracts;

namespace IMDBClone.Domain.Validations
{
    public class RegisterValidator : AbstractValidator<RegisterDTO>
    {
        private readonly IUserService _userService;

        public RegisterValidator(IUserService userService)
        {
            _userService = userService;
            RuleFor(user => user.FullName)
                .NotEmpty()
                .MinimumLength(2)
                .WithMessage("Minimum length of full name is 2 characters")
                .MaximumLength(55)
                .WithMessage("Maximal length of full name is 55 characters");
            RuleFor(user => user.Email)
                .EmailAddress()
                .WithMessage("The entered email address is not valid")
                .NotEmpty()
                .MustAsync(async (email, cancellation) => await IsEmailUnique(email))
                .WithMessage("This email is currently in use");
            RuleFor(user => user.Password)
                .MinimumLength(6)
                .WithMessage("Password too short")
                .Equal(user => user.ConfirmPassword)
                .WithMessage("Passwords should match");
        }
        private async Task<bool> IsEmailUnique(string email)
        {
            return await _userService.GetUserByEmailAsync(email) == null;
        }
    }
}