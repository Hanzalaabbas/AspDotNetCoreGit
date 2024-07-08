using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AspMVCCoreGit.Models
{
    [Keyless]
    public class SignInModel
    {
        [Required,EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display (Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
