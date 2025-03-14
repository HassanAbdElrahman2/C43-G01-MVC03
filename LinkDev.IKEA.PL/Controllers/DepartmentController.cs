using LinkDev.IKEA.BLL.Models.Deoartments;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LinkDev.IKEA.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ILogger<DepartmentController> _logger;
        private readonly IDepartmentService _departmentService;
        // Inheritance : DepartmentController is a Controller
        // Composition : Department has a  IdepartmentService
        #region Services
        public DepartmentController(ILogger<DepartmentController>logger,IDepartmentService departmentService)
        {
            _logger = logger;
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

        #region Create

        [HttpGet]
        public ActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(CreateDepartmentViewModel model)
        {
            var massage = "Department Created Successfully";
            try
            {
                if (!ModelState.IsValid) // Server-Side Validation
                    return View(model);
                
                var departmentToCeate = new CreateDepartmentDto(model.Name, model.Description, model.Code, model.CreationDate);
                var Created = _departmentService.CreateDepartment(departmentToCeate) > 0;
                if (!Created)

                    massage = "Failed To Create Department";
            }
            catch (Exception ex)
            {

                // 1. Log Exception in Database or External File (By Default Logging System In .Net or By Serialog Package)
                _logger.LogError(ex.Message, ex.StackTrace!.ToString());
                // 2. Set Massage
                massage = "An Error Occurred, Please Try Again Later ";
            }
            TempData["Massage"] = massage; 

            return RedirectToAction(nameof(Index));
            
        }

        #endregion
    }
}
