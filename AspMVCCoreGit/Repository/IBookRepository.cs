using AspMVCCoreGit.Models;

namespace AspMVCCoreGit.Repository
{
    public interface IBookRepository
    {
        Task<int> AddNewBook(BookModel bookModel);
        Task<BookModel> GetAllBookById(int Id);
        Task<List<BookModel>> GetAllBooks();
        Task<List<BookModel>> GetTopBooksAsync(int count);
        List<string> ListLanguages();
        List<LanguageModel> ListLanguages1();
        string GetBookName();
    }
}