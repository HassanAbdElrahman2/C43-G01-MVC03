using LinkDev.IKEA.DAL.Common.Enums;
using LinkDev.IKEA.DAL.Entities.Employees.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Models.Employees
{
    public record EmployeeCreateDto
       (string Name, int Age, string? Address, decimal Salary, bool IsActive, string? PhoneNumber, DateTime HiringDate
       , string? Email, Gender Gender, EmployeeType EmployeeType, int? DepartmentId);
}
