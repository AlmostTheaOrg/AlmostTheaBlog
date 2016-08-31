namespace TheaBlog.Validators
{
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Web;

    using FluentValidation.Validators;
    
    public class ImagePropertyValidator<T> : PropertyValidator
    {
        public ImagePropertyValidator() : base("Image property not valid!")
        {
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            object value = context.PropertyValue;
            var file = value as HttpPostedFileBase;
            if (file == null)
            {
                return false;
            }

            try
            {
                using (var img = Image.FromStream(file.InputStream))
                {
                    return img.RawFormat.Equals(ImageFormat.Png) || img.RawFormat.Equals(ImageFormat.Jpeg);
                }
            }
            catch { }

            return false;
        }
    }
}