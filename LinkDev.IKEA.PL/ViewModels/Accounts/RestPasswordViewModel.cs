using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Accounts
{
    public class RestPasswordViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Password Is Required")]
        public required string NewPassword { get; set; }
        [Compare(nameof(NewPassword))]
        [Required(ErrorMessage = "Confirm Password Is Required")]
        [DataType(DataType.Password)]
        public required string ConfirmPassword { get; set; }
    }
}
