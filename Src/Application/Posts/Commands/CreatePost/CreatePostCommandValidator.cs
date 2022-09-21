using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.Posts.Commands.CreatePost
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator()
        {
            RuleFor(x => x)
                .SetValidator(new CheckEmptyPostValidator<CreatePostCommand, CreatePostCommand>(Resources.Messages.Validations.EmptyPost));

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage(string.Format(Resources.Messages.Validations.Required,Resources.DataDictionary.Title))
                .MaximumLength(500)
                .WithMessage(string.Format(Resources.Messages.Validations.TooLength, Resources.DataDictionary.Title))
                ;

            RuleFor(x => x.Content)
                .MaximumLength(15000)
                .WithMessage(string.Format(Resources.Messages.Validations.TooLength, Resources.DataDictionary.Content))
                ;

            RuleFor(x => x.Tags)
                .MaximumLength(500)
                .WithMessage(string.Format(Resources.Messages.Validations.TooLength, Resources.DataDictionary.Tags))

                ;

            When(x => x.PictureFile != null && x.PictureFile.Length > 0, (() =>
            {
                var imgValidator = new FileValidations<CreatePostCommand, IFormFile>()
                {
                    IsRequied                       = false,
                    ExtensionAcceptable             = "jpeg,jpg,gif,png,",
                    ExtensionAcceptableErrorMessage = "The image file format doesn't support",
                    MaximumFileSize                 = 1024*1024*3,
                    MaximumFileSizeErrorMessage     = "The image file is too big",
                    RequiredErrorMessage            = "",
                };

                RuleFor(x => x.PictureFile)
                    .SetValidator(imgValidator);
            }));

            When(x => x.SoundFile != null && x.SoundFile.Length > 0, (() =>
            {
                var soundValidator = new FileValidations<CreatePostCommand, IFormFile>()
                {
                    IsRequied = false,
                    ExtensionAcceptable = "mp3,wav,ogg",
                    ExtensionAcceptableErrorMessage = "The sound file format doesn't support",
                    MaximumFileSize = 1024 * 1024 * 3,
                    MaximumFileSizeErrorMessage = "The sound file is too big",
                    RequiredErrorMessage = "",
                };

                RuleFor(x => x.PictureFile)
                    .SetValidator(soundValidator);
            }));
        }

    }

}
