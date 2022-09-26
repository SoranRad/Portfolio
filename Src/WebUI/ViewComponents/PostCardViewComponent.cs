using Application.Posts.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.ViewComponents
{
    public class PostCardViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(PagedPostDto article, CancellationToken cancellationToken)
        {
            return View(article);
        }
    }
}
