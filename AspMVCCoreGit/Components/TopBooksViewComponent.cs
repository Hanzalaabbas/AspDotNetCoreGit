using AspMVCCoreGit.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AspMVCCoreGit.Components
{
    public class TopBooksViewComponent : ViewComponent
    {
        private readonly IBookRepository _bookRepository = null;

        public TopBooksViewComponent(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
           
        }

        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            var books = await _bookRepository.GetTopBooksAsync(count);
            return View(books);
        }
    }
}
