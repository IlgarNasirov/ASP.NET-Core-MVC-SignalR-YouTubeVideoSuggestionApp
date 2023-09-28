using FluentValidation;
using YouTubeVideoSuggestionApp.DTOs;

namespace YouTubeVideoSuggestionApp.Validators
{
    public class DeleteSuggestionDTOValidator:AbstractValidator<DeleteSuggestionDTO>
    {
        public DeleteSuggestionDTOValidator()
        {
            RuleFor(s => s.Password)
                .NotEmpty()
                .WithMessage("The password field must not be empty.")
                .MinimumLength(6)
                .WithMessage("The password field must be at least 6 characters long.");
        }
    }
}
