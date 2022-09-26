using Application.Posts.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.Post
{
    public class PagingModel : PageModel
    {
        private readonly IMediator _mediator;

        [BindProperty(SupportsGet = true)]
        public int PageNo { get; set; }


        public IEnumerable<PagedPostDto> Posts { get; set; }

        public PagingModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            Posts = await _mediator.Send(new PagedPostQuery(PageNo), cancellationToken);
        }
    }
}
