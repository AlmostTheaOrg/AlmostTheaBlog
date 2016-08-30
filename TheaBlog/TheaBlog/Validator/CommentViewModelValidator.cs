namespace TheaBlog.Validator
{
    using FluentValidation;
    using Models;

    public class CommentViewModelValidator : AbstractValidator<CommentCreateViewModel>
    {
        public CommentViewModelValidator()
        {
            RuleFor(m => m.Description)
             .NotEmpty()
             .WithMessage("Description required!")
             .Length(0, 500)
             .WithMessage($"Description must be below {500} characters!");

            RuleFor(m => m.PhotoId)
                .NotNull()
                .WithMessage("Comment photo bind cannot be null!")
                .NotEmpty()
                .WithMessage("Comment must be binded to valid photo!");

            RuleFor(m => m.AuthorId)
                .NotNull()
                .WithMessage("Comment's author cannot be null!")
                .NotEmpty()
                .WithMessage("Comment must have valid author!");
        }
    }
}