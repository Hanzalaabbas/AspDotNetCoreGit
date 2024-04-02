using AspMVCCoreGit.Models;
using AspMVCCoreGit.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AspMVCCoreGit.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository =null;

        public BookController(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        //public BookController(BookRepository bookRepository) {
        //_bookRepository = bookRepository;
        //}
        public async Task<IActionResult> GetAllBooks()
        {
            var data = await _bookRepository.GetAllBooks();
            return View(data);
        }
        public IActionResult AddNewBook(bool isSuccess =false)
        {
            ViewBag.IsSucces = isSuccess;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            if(ModelState.IsValid) 
            { 
            int id = await _bookRepository.AddNewBook(bookModel);

            if (id > 0)
            {
                return RedirectToAction(nameof(AddNewBook),new { isSuccess = true });
            }
            }
            ModelState.AddModelError("", "This is my Custome Error Message.");
            return View();
        }
        [HttpGet]
        //[Route("book-details/{Id}",Name = "bookDetailsRoute")]
        public async Task<IActionResult> GetBook(int Id)
        {
            var data = await _bookRepository.GetAllBookById(Id);

           
            return View(data);
        }
    }
}
