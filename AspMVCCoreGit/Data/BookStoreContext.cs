using Microsoft.EntityFrameworkCore;
using AspMVCCoreGit.Models;

namespace AspMVCCoreGit.Data
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options) 
            :base(options)
        {
        }
        public DbSet<Books> Books { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<BookGallery> BookGalleries { get; set; }
        //public DbSet<AspMVCCoreGit.Models.BookModel> BookModel { get; set; } = default!;


        //********************We Can Add Connection String Like That in DbContext Code is Start********************
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.,Database=BookStore;Integrated Security=True;");
        //    base.OnConfiguring(optionsBuilder);
        //}
        //********************We Can Add Connection String Like That in DbContext Code is End********************
    }
}
