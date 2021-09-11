using FluentValidation;
using IMDBClone.Domain.DTO.User;

namespace IMDBClone.Domain.Validations
{
    public class LoginValidator: AbstractValidator<LoginDTO>
    {
        public LoginValidator()
        {
            RuleFor(model => model.Username)
                .EmailAddress()
                .WithMessage("Email is not valid");
            RuleFor(model => model.Password)
                .MinimumLength(6)
                .WithMessage("Password is too short");
        }
    }
}