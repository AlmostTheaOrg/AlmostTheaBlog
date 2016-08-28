namespace TheaBlog.Validator
{
    using FluentValidation.Validators;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Linq;
    using System.Web;

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