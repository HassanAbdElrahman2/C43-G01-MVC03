using LinkDev.IKEA.BLL.Models.Employees;
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
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService,ILogger<EmployeeController>logger )
        {
            _employeeService = employeeService;
           _logger = logger;
        }
        #endregion

        #region Index
        public IActionResult Index()
        {
            var Employees = _employeeService.GatEmployees(false).Select(E => new EmployeeViewModel()
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
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var Massage = "Employee Added Successfully";
            try
            {
                var employee = new EmployeeCreateDto(model.Name, model.Age,
                    model.Address, model.Salary, model.IsActive,
                    model.PhoneNumber, model.HiringDate, model.Email,
                    model.Gender, model.EmployeeType,model.DepartmentId);
                var IsCreate = _employeeService.CreateEmployee(employee) > 0;
                if (!IsCreate)
                {
                    Massage = "Employee is not Added !!!";
                }
            }
            catch (Exception ex)
            {

                // 1. Log Exception in Database or External File (By Default Logging System In .Net or By Serialog Package)
                _logger.LogError(ex.Message, ex.StackTrace!.ToString());
                // 2. Set Massage
                Massage = "An Error Occurred, Please Try Again Later ";
            }
            TempData["Massage"] = Massage;
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Details
        public IActionResult Details([FromRoute] int? Id)
        {
            if (!Id.HasValue)
                return BadRequest();
            var E = _employeeService.GetEmployeeById(Id.Value);
            if (E is null)
                return NotFound();
            var EmployeeView = new EmployeeDetailsViewModel()
            {
                Id = Id.Value,
                Name = E.Name,
                Age = E.Age,
                Salary = E.Salary,
                IsActive = E.IsActive,
                Email = E.Email,
                Gender = (Gender)Enum.Parse(typeof(Gender), E.Gender),
                EmployeeType = (EmployeeType)Enum.Parse(typeof(EmployeeType), E.EmployeeType),
                CreatedBy = E.CreatedBy,
                LastModifiedBy = E.LastModifiedBy,
                CreatedOn = E.CreatedOn,
                LastModifiedOn = E.LastModifiedOn,
                PhoneNumber = E.PhoneNumber,
                Address = E.Address,
                HiringDate = E.HiringDate,
               
            };
            return View(EmployeeView);

        }
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            if (!Id.HasValue)
                return BadRequest();
            var E = _employeeService.GetEmployeeById(Id.Value);
            if (E is null)
                return NotFound();
            var Employee = new EmployeeUpdateViewModel()
            {
                Id = Id.Value,
                Name = E.Name,
                Age = E.Age,
                Salary = E.Salary,
                IsActive = E.IsActive,
                Email = E.Email,
                Address = E.Address,
                EmployeeType = (EmployeeType)Enum.Parse(typeof(EmployeeType), E.EmployeeType),
                Gender = (Gender)Enum.Parse(typeof(Gender), E.Gender),
                HiringDate = E.HiringDate,
                PhoneNumber = E.PhoneNumber,
                DepartmentId=E.DepartmentId
                
            };
            TempData["Id"] = Id;
            return View(Employee);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeUpdateViewModel Model)
        {
            if (!ModelState.IsValid)
                return View(Model);
            if (!((int?)TempData["Id"] == Model.Id))
                return NotFound();
            var Massage = "Employee Update Successfully";
            try
            {
                var Employee = new EmployeeUpdateDto(Model.Id, Model.Name, Model.Age, Model.Address, Model.Salary, Model.IsActive, Model.PhoneNumber, Model.HiringDate, Model.Email, Model.Gender, Model.EmployeeType,Model.DepartmentId);
                var IsUdeted = _employeeService.UpdateEmployee(Employee) > 0;
                if (!IsUdeted)
                    Massage = "Failed to update Department";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace!.ToString());
                Massage = "An Error Occurred, Please Try Again Later ";

            }
            TempData["Massage"] = Massage;
            return RedirectToAction(nameof(Index));

        }
        #endregion

        #region Delete
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var message = "Employee Deleted Successfully";
            try
            {
                var deleted = _employeeService.DeleteEmployee(id);
                if (!deleted)
                    message = "Failed to Delete Employee";
            }
            catch (Exception ex)
            {
                // 1. Log Exception in Database or External File (By Default Logging System in .NET or any other)
                _logger.LogError(ex.Message, ex.StackTrace!.ToString());
                // 2. Set Message
                message = "An Error Occurred, Please Try Again Later";
            }

            TempData["Massage"] = message;
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
