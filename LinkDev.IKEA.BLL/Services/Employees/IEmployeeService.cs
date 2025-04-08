using LinkDev.IKEA.BLL.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.Employees
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GatEmployees(bool WithTracking );
        EmployeeDetailsDto? GetEmployeeById(int id);
        int CreateEmployee(EmployeeCreateDto employee);
        int UpdateEmployee(EmployeeUpdateDto employee);
        bool DeleteEmployee(int id);
    }
}
