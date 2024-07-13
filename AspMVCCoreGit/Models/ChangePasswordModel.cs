using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AspMVCCoreGit.Models
{
    [Keyless]
    public class ChangePasswordModel
    {
        [Required,DataType(DataType.Password),Display(Name="Current Password")]
        public required string CurrentPassword { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "New Password")]
        public required string NewPassword { get; set; }
        [Required, DataType(DataType.Password), Display(Name = "Confirm Password")]
        [Compare("NewPassword", ErrorMessage ="Confirm new password does not match")]
        public required string ConfirmPassword { get; set; }
    }
}
