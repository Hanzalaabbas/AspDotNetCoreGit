using System.ComponentModel.DataAnnotations;

namespace AspMVCCoreGit.Enums
{
    public enum LanguageEnum
    {
        [Display (Name =" Hindi Language")]
        Hindi=1 ,
        [Display(Name = " English Language")]
        English =2,
        [Display(Name = " Germen Language")]
        German =3,
        [Display(Name = " Urdu Language")]
        Urdu =4,
        [Display(Name = " Punjabi Language")]
        Punjabi =5
    }
}
