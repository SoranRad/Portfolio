using Application.Common.Interfaces;
using Domain.Aggregates.Posts;
using Domain.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Http;
using SharedKernel.Result;

namespace Application.Posts.Commands.CreatePost
{
    public class CreatePostCommand : IRequest<Result>
    {
        public string       Title               { get; set; }
        public string?      Content             { get; set; }
        public bool         IsContentFirst      { get; set; }
        public string?      Tags                { get; set; }

        public IFormFile?    PictureFile         { get; set; }
        public IFormFile?    SoundFile           { get; set; }
    }

    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand,Result>
    {
        private readonly IGenericRepository<Post>   _repository;
        private readonly IAttachementSave           _attachementSave;

        public CreatePostCommandHandler
            (
                IGenericRepository<Post>    repository,
                IAttachementSave            attachementSave
            )
        {
            _repository         = repository;
            _attachementSave    = attachementSave;
        }
        public async Task<Result> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            ////================== Find Filename Attachment ================
            //var fileName = string.Empty;

            //if (request.SoundFile == null && request.PictureFile == null)
            //    fileName = null;
            //else if (request.SoundFile != null)
            //    fileName = request.SoundFile.FileName;
            //else if (request.PictureFile != null)
            //    fileName = request.PictureFile.FileName;

            ////=================== Create New Post ===============

            //var result  = Post.Create
            //    (
            //        Content         : request.Content, 
            //        Title           : request.Title,
            //        IsContentFirst  : request.IsContentFirst,
            //        Tags            : request.Tags,
            //        IsNullPicture   : request.PictureFile == null   || request.PictureFile.Length == 0,
            //        IsNullSound     : request.SoundFile == null     || request.SoundFile.Length == 0,
            //        FileName        : fileName
            //    );
            
            //if (result.IsSuccess == false)
            //    return result;

            ////=================== Add And Save ===============

            //_repository.Add(result.Value);
            //await _repository.SaveChangesAsync();

            ////=================== Save Attachment File ===============

            //if (request.PictureFile != null || request.SoundFile != null)
            //    await _attachementSave.Save(result.Value.Id, (request.PictureFile ?? request.SoundFile)!);


            return Result.Success();
        }
    }
}

