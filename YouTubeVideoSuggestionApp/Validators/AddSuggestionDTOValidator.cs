using FluentValidation;
using YouTubeVideoSuggestionApp.DTOs;

namespace YouTubeVideoSuggestionApp.Validators
{
    public class AddSuggestionDTOValidator:AbstractValidator<AddSuggestionDTO>
    {
        public AddSuggestionDTOValidator()
        {
            RuleFor(s => s.Username)
                .NotEmpty()
                .WithMessage("The username field must not be empty.")
                .MinimumLength(3)
                .WithMessage("The username field must be at least 3 characters long.")
                .MaximumLength(25)
                .WithMessage("The username field cannot exceed 25 characters.");

            RuleFor(s=>s.ShortDescription)
                .MaximumLength(50)
                .WithMessage("The short description field cannot exceed 50 characters.");

            RuleFor(s=>s.Password)
                .NotEmpty()
                .WithMessage("The password field must not be empty.")
                .MinimumLength(6)
                .WithMessage("The password field must be at least 6 characters long.");

            RuleFor(s => s.YouTubeUrl)
                .NotEmpty()
                 .WithMessage("The YouTube URL field must not be empty.")
                .MaximumLength(50)
                .Matches(@"^(https?\:\/\/)?(www\.youtube\.com|youtu\.be)\/.+$")
                .WithMessage("The YouTube URL field is invalid.");
        }
    }
}
