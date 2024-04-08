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
            var allBooks = await _context.Books.Include(b => b.Language).AsNoTracking().ToListAsync();
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
                        LanguageId = book.LanguageId,
                        Language = book.Language.Name
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
                LanguageId =bookModel.LanguageId    ,
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

            var data=  await _context.Books.Where(x => x.Id == Id).Select(book => new BookModel()
            {
                Author = book.Author,
                Title = book.Title,
                TotalPages = book.TotalPages.HasValue ? book.TotalPages.Value : 0,//this condition is put here if totalpages have any value then ok other wise set zero
                Description = book.Description,
                LanguageId = book.LanguageId,
                Category = book.Category,
                Id = book.Id,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                Language = book.Language.Name
            }).FirstOrDefaultAsync();
            return data;


            //if( book is not null)
            //{
            //    var bookDetail = new BookModel()
            //    {
            //        Author = book.Author,
            //        Title = book.Title,
            //        TotalPages = book.TotalPages.HasValue ?book.TotalPages.Value :0,//this condition is put here if totalpages have any value then ok other wise set zero
            //        Description = book.Description,
            //        LanguageId = book.LanguageId,
            //        Category = book.Category,
            //        Id = book.Id,
            //        CreatedOn = DateTime.UtcNow,
            //        UpdatedOn = DateTime.UtcNow,
            //        Language = book.Languages.Name
            //    };
            //    return bookDetail;
            //}
            //return null; ;
        }
        public List<LanguageModel> ListLanguages1()
        {
            List<LanguageModel> data = new List<LanguageModel>()
            {
                new LanguageModel(){Id=1,Name= "Hindi"},
                new LanguageModel(){Id=2,Name="English" },
                new LanguageModel(){Id=3,Name="Urdu" },
                new LanguageModel(){Id=4,Name="Punjabi"}
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
