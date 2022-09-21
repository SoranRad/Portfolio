using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Posts.Commands.CreatePost
{
    public class CreatePostCommand : IRequest
    {
        public string       Title               { get; set; }
        public string       Content             { get; set; }
        public bool         IsContentFirst      { get; set; }
        public string       Tags                { get; set; }

        public IFormFile    PictureFile         { get; set; }
        public IFormFile    SoundFile           { get; set; }
    }

    public class CreatePostCommandHandler : AsyncRequestHandler<CreatePostCommand>
    {
        private readonly IGenericRepository<Domain.Aggregates.Posts.Posts> _repository;

        public CreatePostCommandHandler(IGenericRepository<Domain.Aggregates.Posts.Posts> repository)
        {
            _repository = repository;
        }
        protected async override Task Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            



            return;
        }
    }
}

