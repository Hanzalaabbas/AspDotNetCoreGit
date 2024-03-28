using AspMVCCoreGit.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspMVCCoreGit.Controllers
{
    public class BookController : Controller
    {
        public IActionResult AddNewBook()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddNewBook(BookModel bookModel)
        {
            return View();
        }
    }
}
