using AspMVCCoreGit.Models;
using AspMVCCoreGit.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspMVCCoreGit.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository =null;
        private readonly LanguageRepository _languageRepository = null;

        public BookController(BookRepository bookRepository, LanguageRepository languageRepository)
        {
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
        }
        //public BookController(BookRepository bookRepository) {
        //_bookRepository = bookRepository;
        //}
        public async Task<IActionResult> GetAllBooks()
        {
            var data = await _bookRepository.GetAllBooks();
            return View(data);
        }
        public async Task<IActionResult> AddNewBook(bool isSuccess =false)
        {
            ViewBag.IsSucces = isSuccess;
            ViewBag.Language =new SelectList(_bookRepository.ListLanguages());
            ViewBag.Language1 = _bookRepository.ListLanguages1().Select(x => new SelectListItem() 
            {
                Text = x.Name,
                Value = x.Id.ToString() ,
                Selected = true

            });
            var group1 = new SelectListGroup() { Name = "Group 1" };
            var group2 = new SelectListGroup() { Name = "Group 2" };
            var group3 = new SelectListGroup() { Name = "Group 3" };
            //**********************This is DropDownList using SelectListItem disabled and Selected code is End * *********************
            ViewBag.Language2 = new List<SelectListItem>()
            {
                new SelectListItem() {Text="Hindi",Value="1",Group =group1},
                new SelectListItem() {Text="English",Value="2",Group =group2},
                new SelectListItem() {Text="Urdu",Value="3",Group =group3},
                new SelectListItem() {Text="Punjabi",Value="4",Selected=true,Disabled=true,Group =group3},
            };
            //**********************This is Multiple Select DropDownList using SelectListItem disabled and Selected code is Start * *********************
            ViewBag.Language3 = new List<SelectListItem>()
            {
                new SelectListItem() {Text="Hindi",Value="1"},
                new SelectListItem() {Text="English",Value="2"},
                new SelectListItem() {Text="Urdu",Value="3"},
                new SelectListItem() {Text="Punjabi",Value="4"},
            };
            //**********************This is Multiple Select  DropDownList using SelectListItem disabled and Selected code is End * *********************
            //**********************This is   DropDownList using get data from database  code is Start * *********************
            ViewBag.Language4 = new SelectList(await _languageRepository.GetLanguages(),"Id","Name");
            //**********************This is   DropDownList using get data from database  code is End * *********************
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
            ViewBag.Language =new SelectList(_bookRepository.ListLanguages());
            ViewBag.Language1 = _bookRepository.ListLanguages1().Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),

            });
            //**********************This is DropDownList using SelectListItem disabled and Selected code is Start * *********************
            ViewBag.Language2 = new List<SelectListItem>()
            {
                new SelectListItem() {Text="Hindi",Value="1",Disabled=true},
                new SelectListItem() {Text="English",Value="2"},
                new SelectListItem() {Text="Urdu",Value="3"},
                new SelectListItem() {Text="Punjabi",Value="4",Selected=true,Disabled=true},
            };
            //**********************This is DropDownList using SelectListItem disabled and Selected code is End * *********************
            //**********************This is Multiple Select DropDownList using SelectListItem disabled and Selected code is Start * *********************
            ViewBag.Language3 = new List<SelectListItem>()
            {
                new SelectListItem() {Text="Hindi",Value="1"},
                new SelectListItem() {Text="English",Value="2"},
                new SelectListItem() {Text="Urdu",Value="3"},
                new SelectListItem() {Text="Punjabi",Value="4"},
            };
            //**********************This is Multiple Select  DropDownList using SelectListItem disabled and Selected code is End * *********************
            //**********************This is   DropDownList using get data from database  code is Start * *********************
            ViewBag.Language4 = new SelectList(await _languageRepository.GetLanguages(), "Id", "Name");
            //**********************This is   DropDownList using get data from database  code is End * *********************
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
