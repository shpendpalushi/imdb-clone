using System.Data;
using FluentValidation;
using IMDBClone.Domain.DTO;

namespace IMDBClone.Domain.Validations
{
    public class MovieValidator : AbstractValidator<MovieDTO>
    {
        public MovieValidator()
        {
            RuleFor(model => model.Title)
                .MinimumLength(2)
                .WithMessage("Title should be at least 2 characters");
            RuleFor(model => model.Cast)
                .Must(cast => cast.Count>=2)
                .WithMessage("Movie should have at least 2 actors");
            RuleFor(m => m.ReleaseDate)
                .NotNull()
                .WithMessage("Release Date should not be null");
        }
    }
}