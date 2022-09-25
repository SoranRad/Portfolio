using Application.Posts.Commands.CreatePost;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Result;

namespace WebUI.Api
{
    public class PostsController : BaseApi
    {
        private readonly IMediator _mediator;

        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<Result> Get(CancellationToken cancellationToken)
        {
            return Result.Success("OK");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<Result> Post([FromForm] CreatePostCommand form, CancellationToken cancellationToken)
        {
             await _mediator.Send(form, cancellationToken);

             return Result.Success
                 (
                     string.Format(Resources.Messages.Successes.SuccessCreate,Resources.DataDictionary.Post)
                 );
        }

    }
}
