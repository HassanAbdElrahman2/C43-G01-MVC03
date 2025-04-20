using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Users
{
    public class UserEditViewModel
    {
        public string Id { get; set; } = null!;
        [Required]
        [Display(Name = "Frist Name")]
        public string FName { get; set; } = null!;
        [Display(Name = "Last Name")]
        public string? LName { get; set; } = null!;
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }
    }
}
