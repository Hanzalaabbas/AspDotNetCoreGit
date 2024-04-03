using AspMVCCoreGit.Data;
using AspMVCCoreGit.Models;
using Microsoft.EntityFrameworkCore;

namespace AspMVCCoreGit.Repository
{
    public class BookRepository
    {
        private readonly BookStoreContext? _context = null;

        public BookRepository(BookStoreContext context) {
            _context = context;
        }
        public async Task<List<BookModel>> GetAllBooks()
        {
            var books =new List<BookModel>();
            var allBooks = await _context.Books.AsNoTracking().ToListAsync();
            if (allBooks.Any() == true) {
            foreach (var book in allBooks)
                {
                    books.Add(new BookModel()
                    {
                        Title = book.Title,
                        Author = book.Author,
                        TotalPages = (int)book.TotalPages,
                        Category = book.Category,
                        Id = book.Id,
                        Description = book.Description,
                        Language = book.Language,
                    });
                }
            }
            return books;
        }
        
       public async Task<int> AddNewBook(BookModel bookModel)
        {
            var newBook = new Books()
            {
                Author = bookModel.Author,
                Title = bookModel.Title,
                TotalPages = bookModel.TotalPages,
                Language =bookModel.Language,
                Description = bookModel.Description,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,

            };
           await _context.Books.AddAsync(newBook);
           await _context.SaveChangesAsync();
            return newBook.Id;
        }
        public async Task<BookModel> GetAllBookById(int Id)
        {

            var book = await _context.Books.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if( book is not null)
            {
                var bookDetail = new BookModel()
                {
                    Author = book.Author,
                    Title = book.Title,
                    TotalPages = book.TotalPages.HasValue ?book.TotalPages.Value :0,//this condition is put here if totalpages have any value then ok other wise set zero
                    Description = book.Description,
                    Language = book.Language,
                    Category = book.Category,
                    Id = book.Id,
                    CreatedOn = DateTime.UtcNow,
                    UpdatedOn = DateTime.UtcNow,
                };
                return bookDetail;
            }
            return null; ;
        }
        public List<LanguageModel> ListLanguages1()
        {
            List<LanguageModel> data = new List<LanguageModel>()
            {
                new LanguageModel(){Id=1,Text= "Hindi"},
                new LanguageModel(){Id=2,Text="English" },
                new LanguageModel(){Id=3,Text="Urdu" },
                new LanguageModel(){Id=4,Text="Punjabi"}
            }.ToList();
            return data;
        }
        public  List<String> ListLanguages()
        {
            List<string> data=  new List<string>() { "Hindi", "English", "Urdu", "Punjabi" };
            return data;
        }
    }
}
