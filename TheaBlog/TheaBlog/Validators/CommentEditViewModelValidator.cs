namespace TheaBlog.Validators
{
    using FluentValidation;
    using Models.ViewModels;

    public class CommentEditViewModelValidator : AbstractValidator<CommentEditViewModel>
    {
        public CommentEditViewModelValidator()
        { 
            RuleFor(m => m.Description)
                .NotNull()
                .NotEmpty()
                .WithMessage("Comment must have description")
                .Length(1,500)
                .WithMessage($"Comment's description must be between {1} and {500} symbols long.");
        }
    }
}