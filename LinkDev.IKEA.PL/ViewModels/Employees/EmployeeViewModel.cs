using LinkDev.IKEA.DAL.Common.Enums;
using LinkDev.IKEA.DAL.Entities.Employees.Enums;
using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Employees
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "Max Lenth Should Be 50 Character")]
        [MinLength(5, ErrorMessage = "Min Lenth Should Be 5 Character")]
        public required string Name { get; set; }
        [Range(22, 30)]
        public int? Age { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public Gender Gender { get; set; }
        [Display(Name = "Type")]
        public EmployeeType EmployeeType { get; set; }
        [Display(Name = "Department")]
        public string? DepartmetName { get; set; }
    }
}
