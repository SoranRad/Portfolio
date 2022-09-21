using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Extention;
using FluentValidation;

namespace Application.Posts.Commands.CreatePost
{
    public class CheckEmptyPostValidator<T, TProperty> : PropertyValidator<T, TProperty>
    {
        private readonly string _messsage;

        public CheckEmptyPostValidator(string Messsage)
        {
            _messsage = Messsage;
        }
        public override bool IsValid(ValidationContext<T> context, TProperty value)
        {
            var post = value as CreatePostCommand ;

            var result = post.PictureFile != null
                         || post.SoundFile != null
                         || !post.Content.IsHtmlEmpty();

            if(!result)
                context.AddFailure(context.PropertyName, _messsage);

            return result;
        }

        public override string Name { get; }
    }
}
