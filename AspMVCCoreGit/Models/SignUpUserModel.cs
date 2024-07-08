using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AspMVCCoreGit.Models
{
    [Keyless]
    public class SignUpUserModel
    {
        [Required(ErrorMessage = "Please Enter Your First Name")]
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        [Required (ErrorMessage="Please Enter Your Email")]
        [Display(Name ="Email Address")]
        [EmailAddress (ErrorMessage ="Please Enter a Valid Email")]
        
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Your Strong Password")]
        [Display(Name = "Password")]
        [Compare("ConfirmPassword", ErrorMessage ="Password Does Not Match")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Enter Your Confirm Password")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
