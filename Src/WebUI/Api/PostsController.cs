using Application.Posts.Commands.CreatePost;
using Application.Posts.Queries;
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

        //[HttpGet]
        //public async Task<Result> Get(CancellationToken cancellationToken)
        //{
        //    return Result.Success("OK");
        //}

        //[HttpGet("{page}")]
        //public async Task<Result<IEnumerable<PagedPostDto>>> GetPage(int page, CancellationToken cancellationToken)
        //{
        //    var list = await _mediator.Send(new PagedPostQuery(page), cancellationToken);

        //    return Result.Success(list);
        //}

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
