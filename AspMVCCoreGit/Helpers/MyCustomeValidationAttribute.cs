using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace AspMVCCoreGit.Helpers
{
    public class MyCustomeValidationAttribute : ValidationAttribute
    {
        public MyCustomeValidationAttribute(string _text) { 
            Text = _text;
        }
        public string Text { get; set; }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value != null)
            { string bookName = value.ToString();
                if (bookName.Contains(Text))
                {
                    return ValidationResult.Success;
                }

            }
            return new ValidationResult(ErrorMessage ??"Book Does not contain the desired value");
        }
    }
}
