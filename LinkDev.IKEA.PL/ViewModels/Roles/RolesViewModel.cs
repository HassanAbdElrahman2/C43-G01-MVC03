using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Roles
{
    public class RolesViewModel
    {
        [Required]
        public required string Id { get; set; } 
        [Required]
        public required string Name { get; set; }

        public RolesViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
