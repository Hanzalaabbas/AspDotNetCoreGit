using AspMVCCoreGit.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspMVCCoreGit.Models
{
    public class BookModel
    {
        //[DataType(DataType.Password)]
        //public string? MyField { get; set; } 


        public int Id { get; set; }
        [StringLength (100,MinimumLength = 3,ErrorMessage ="Book Title length is minimum atleast 3 and maximum is 100")]
        [Required (ErrorMessage ="Book Title is Required")]
        
        public string? Title { get; set; }
        [StringLength(100, MinimumLength = 13, ErrorMessage = "Book Title length is minimum atleast 13 and maximum is 100")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Book Author Name is Required")]
        public string? Author { get; set; }
        public string? Category { get; set;}
        public int LanguageId { get; set;}
        public string? Language { get; set; }

        public List<string>? MultipleLanguage { get; set; }
        [Required(ErrorMessage = "Please choose the language of the book")]
        public LanguageEnum LanguageEnum { get; set; }
        [Required(ErrorMessage = "Book Total Pages is Required")]

        [Display(Name = "Total Number of Pages")]
        public int TotalPages { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}

