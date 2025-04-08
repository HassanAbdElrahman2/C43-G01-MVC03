using LinkDev.IKEA.BLL.Services.Employees;
using LinkDev.IKEA.DAL.Common.Enums;
using LinkDev.IKEA.DAL.Entities.Employees.Enums;
using LinkDev.IKEA.PL.ViewModels.Employees;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    public class EmployeeController : Controller
    {
        #region Services
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        } 
        #endregion
        public IActionResult Index()
        {
            var Employees=_employeeService.GatEmployees(false).Select(E => new EmployeeViewModel()
            {
                Id = E.Id,
                Name = E.Name,
                Age = E.Age,
                Salary = E.Salary,
                IsActive = E.IsActive,
                Email = E.Email,
                Gender = (Gender)Enum.Parse(typeof(Gender), E.Gender),
                EmployeeType = (EmployeeType)Enum.Parse(typeof(EmployeeType), E.EmployeeType),
            }); ;
            return View(Employees);
        }
    }
}
