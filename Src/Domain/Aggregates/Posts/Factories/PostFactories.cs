using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.SharedKernel.Extention;
using SharedKernel.Result;

namespace Domain.Aggregates.Posts
{
    public partial class Post
    {
        public static Result<Post> Create
            (
                string  Content,
                string  Title,
                bool    IsContentFirst,
                string  Tags,
                bool    IsNullPicture,
                bool    IsNullSound,
                string  FileName
                    )
        {
            if (string.IsNullOrWhiteSpace(Title))
            {
                return Result.Fail<Post>
                (
                    nameof(Post.Title),
                    string.Format(Resources.Messages.Validations.Required, nameof(Post.Title))
                );
            }

            if (Title.Length > Post.MAX_TITLE_LENGTH)
            {
                return Result.Fail<Post>
                (
                    nameof(Post.Title),
                    string.Format(Resources.Messages.Validations.MaxLength, nameof(Post.Title), Post.MAX_TITLE_LENGTH)
                );
            }

            if (
                Content.IsHtmlEmpty()
                && IsNullPicture
                && IsNullSound
            )
            {
                return Result.Fail<Post>
                (
                    Resources.Messages.Validations.EmptyPost
                );
            }


            var newPost = new Post
                (
                content:Content,
                title:Title,
                tags:Tags,
                isContentFirst:IsContentFirst,
                fileName:FileName
                );
            return Result.Success(newPost);
        }
    }
}
