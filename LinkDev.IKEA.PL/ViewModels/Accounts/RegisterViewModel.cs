using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace LinkDev.IKEA.PL.ViewModels.Accounts
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="First Name Can Not Be Null")]
        [MaxLength(50)]
        [Display(Name ="First Name")]
        public required string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name Can Not Be Null")]
        [MaxLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = null!;
        [Required()]
        [MaxLength(50)]
        [Display(Name = "User Name")]
        public string UserName { get; set; } = null!;
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = null!;
        [Display(Name = "Is Agree")]
        public bool IsAgree { get; set; }

    }
}
