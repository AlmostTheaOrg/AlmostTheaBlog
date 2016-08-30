

namespace TheaBlog.Validator
{
    using FluentValidation;
    using System.Web;
    using TheaBlog.Models;

    public class PhotoViewModelValidator : AbstractValidator<CreatePhotoViewModel>
    {
        public PhotoViewModelValidator() 
        {
            RuleFor(m => m.Title)
                 .NotEmpty()
                 .WithMessage("Title required!")
                 .Length(3, 100)
                 .WithMessage($"Title must between {3} and {100} characters!");

            RuleFor(m => m.Category)
                 .NotEmpty()
                 .WithMessage("Category required!")
                 .Length(3, 100)
                 .WithMessage($"Category must between {3} and {100} characters!");

            RuleFor(m => m.Description)
               .NotEmpty()
               .WithMessage("Description required!")
               .Length(0, 500)
               .WithMessage($"Description must be below {500} characters!");

            RuleFor(m => m.Image)
                .NotNull()
                .WithMessage("Image required!")
                .SetValidator(new ImagePropertyValidator<HttpPostedFileBase>())
                .WithMessage("Image not valid format!");
        }
    }
}