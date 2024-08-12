using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AspMVCCoreGit.Models
{
    [Keyless]
    public class ForgotPasswordModel
    {
        [Required,EmailAddress,Display(Name ="Registerd Email Addess")]
        public string Email { get; set; }
        public bool EmailSent { get; set; }
    }
}
