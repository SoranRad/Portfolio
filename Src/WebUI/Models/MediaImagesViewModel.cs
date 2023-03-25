using Application.Common.Validators;
using Application.Posts.Commands.CreatePost;
using FluentValidation;

namespace WebUI.Models
{
    public class MediaImagesViewModel
    {
        public IFormFile Image { get; set; }
    }

    public class MediaImagesValidation : AbstractValidator<MediaImagesViewModel>
    {
        public MediaImagesValidation()
        {

            When(x => x.Image != null && x.Image.Length > 0, (() =>
            {
                var imgValidator = new FileValidations<MediaImagesViewModel, IFormFile>()
                {
                    IsRequied = false,
                    ExtensionAcceptable = "jpeg,jpg,gif,png,bmp",
                    ExtensionAcceptableErrorMessage = "The image file format doesn't support",
                    MaximumFileSize = 1024 * 1024 * 3,
                    MaximumFileSizeErrorMessage = "The image file is too big",
                    RequiredErrorMessage = "",
                };

                RuleFor(x => x.Image)
                    .SetValidator(imgValidator);
            }));
             
        }
    }



}
