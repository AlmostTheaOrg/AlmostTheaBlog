namespace TheaBlog.Validators
{
    using FluentValidation;
    using Models.ViewModels;

    public class CommentCreateViewModelValidator : AbstractValidator<CommentCreateViewModel>
    {
        public CommentCreateViewModelValidator()
        {
            RuleFor(m => m.Description)
             .NotEmpty()
             .WithMessage("Description required!")
             .Length(0, 500)
             .WithMessage($"Description must be below {500} characters!");
        }
    }
}