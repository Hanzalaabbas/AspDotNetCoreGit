using Microsoft.EntityFrameworkCore;
using AspMVCCoreGit.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AspMVCCoreGit.Data
{
    public class BookStoreContext : IdentityDbContext<ApplicationUser> //by "defult if we not anything" pass then "IdentityDbContext" class think we are working with "IdentityUser" class but now in our case we want to increase some columns then its complosry where we added new colums add in new class in our case my additional column class is "ApplicationUser"
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options) 
            :base(options)
        {
        }
        public DbSet<Books> Books { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<BookGallery> BookGalleries { get; set; }
        public DbSet<SignUpUserModel> SignUpUsers { get; set; }

       
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
