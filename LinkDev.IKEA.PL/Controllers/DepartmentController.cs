using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        // Inheritance : DepartmentController is a Controller
        // Composition : Department has a  IdepartmentService
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var departments= _departmentService.GetDepartments();
            var DepartmentViewModel = departments.Select(D => new DepartmentViewModel() { Code = D.Code, Id = D.Id, Name = D.Name, CreationDate = D.CreationDate });
            return View(DepartmentViewModel);
        }
    }
}
