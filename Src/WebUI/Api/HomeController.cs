using Application.Posts.Commands.CreatePost;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Result;

namespace WebUI.Api
{
    public class HomeController : BaseApi
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Result> Get()
        {
            await _mediator.Send(new CreatePostCommand());
            return Result.Success();
        }
    }
}
