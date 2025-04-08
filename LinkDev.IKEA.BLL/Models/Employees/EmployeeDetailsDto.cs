using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Models.Employees
{
    public record EmployeeDetailsDto
         (int Id, string Name, int? Age, string? Address, decimal Salary, bool IsActive, string? PhoneNumber, DateOnly HiringDate
         , string? Email, string Gender, string EmployeeType, string CreatedBy, DateTime CreatedOn, string LastModifiedBy, DateTime LastModifiedOn);
}
