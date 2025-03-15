using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Departments
{
    public class UpdateDepartmentViewModel
    {

        public int Id { get; set; }
        public required string Name { get; set; }
        [Required(ErrorMessage = "Code Is requied ya Hamada")]
        public required string Code { get; set; }
        [Display(Name = "Creation Date")]
        public DateOnly CreationDate { get; set; }
        public string? Description { get; set; }
    }
}