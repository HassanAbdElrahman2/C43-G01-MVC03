using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LinkDev.IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        // Inheritance : DepartmentController is a Controller
        // Composition : Department has a  IdepartmentService
        #region Services
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        } 
        #endregion

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var departments = _departmentService.GetDepartments();
            var DepartmentViewModel = departments.Select(D => new DepartmentViewModel() { Code = D.Code, Id = D.Id, Name = D.Name, CreationDate = D.CreationDate });
            return View(DepartmentViewModel);
        }
        #endregion

        #region Details
        public IActionResult Details (int? id)
        {
            if (!id.HasValue)
                return BadRequest();
            var department=_departmentService.GetDepartmentById(id.Value);

            if (department is null)
                return NotFound();
            var departmentViewModel = new DepartmentDetailsViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description??"",
                CreationDate = department.CreationDate,
                CreatedBy = department.CreatedBy,
                CreatedOn = department.CreatedOn,
                LastModifiedBy = department.LastModifiedBy,
                LastModifiedOn = department.LastModifiedOn



            };
            return View(departmentViewModel);
        }
        #endregion
    }
}
