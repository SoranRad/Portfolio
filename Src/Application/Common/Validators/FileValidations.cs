using FluentValidation;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwentyDevs.MimeTypeDetective;

namespace Application.Common.Validators
{
    public class FileValidations<T, TProperty> : PropertyValidator<T, TProperty>
    {
        public bool     IsRequied                           { get; set; } = false;

        public string   RequiredErrorMessage                { get; set; }

        public int      MaximumFileSize                     { get; set; }

        public string   MaximumFileSizeErrorMessage         { get; set; }

        public string   ExtensionAcceptable                 { get; set; }

        public string   ExtensionAcceptableErrorMessage     { get; set; }

        private IEnumerable<string> Extensions
        {
            get
            {
                return ExtensionAcceptable.Split(',')
                    .Select(x =>
                        x.Trim().StartsWith('.')
                            ? x.Trim().ToLower()
                            : "." + x.Trim().ToLower()
                    );
            }
        }


        public override bool IsValid(ValidationContext<T> context, TProperty value)
        {
            var file = value as IFormFile;

            if (file == null || file.Length == 0)
            {
                context.AddFailure(context.PropertyName, RequiredErrorMessage);
                return false;
            }

            if (file.Length > MaximumFileSize)
            {
                context.AddFailure(context.PropertyName, MaximumFileSizeErrorMessage);
                return false;
            }

            using (var stream = file.OpenReadStream())
            {
                var mimetype = stream.GetMimeType();

                if (mimetype == null)
                {
                    context.AddFailure(context.PropertyName, ExtensionAcceptableErrorMessage);
                    return false;
                }

                if (!Extensions.Contains(mimetype.Extension))
                {
                    context.AddFailure(context.PropertyName, ExtensionAcceptableErrorMessage);
                    return false;
                }
            }

            return true;
        }

        public    override string Name      { get; }

        protected override string GetDefaultMessageTemplate(string errorCode) => "";
    }
}
