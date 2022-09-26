using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Aggregates.Posts;
using Domain.SharedKernel;
using MapsterMapper;
using MediatR;

namespace Application.Posts.Queries
{
    public class PagedPostQuery : IRequest<IEnumerable<PagedPostDto>>
    {
        public int PageNumber { get; }

        public PagedPostQuery(int pageNumber)
        {
            PageNumber = pageNumber;
        }
    }


    public class PagedPostQueryHandler : IRequestHandler<PagedPostQuery, IEnumerable<PagedPostDto>>
    {
        private readonly IReadRepository<Post> _readRepository;
        private readonly IMapper _mapper;

        public PagedPostQueryHandler
            (
            IReadRepository<Post> readRepository,
            IMapper mapper
            )
        {
            _readRepository = readRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PagedPostDto>> Handle(PagedPostQuery request, CancellationToken cancellationToken)
        {
            var list = _readRepository.GetPaged
            (
                    new PagedResult<Post>()
                    {
                        CurrentPage = request.PageNumber,
                        PageSize = 10,
                    },
                    null, 
                    null
                );

            return _mapper.Map<IEnumerable<PagedPostDto>>(list.Results);

        }
    }

}
