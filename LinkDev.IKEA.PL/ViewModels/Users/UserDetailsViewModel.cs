using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Users
{
    public class UserDetailsViewModel
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
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; } = null!;
    }
}
