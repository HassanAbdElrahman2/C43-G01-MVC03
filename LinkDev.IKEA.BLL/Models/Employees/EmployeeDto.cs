using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Models.Employees
{
    public record EmployeeDto(int Id, string Name, int? Age, decimal Salary, bool IsActive, string? Email, string Gender, string EmployeeType);
}
