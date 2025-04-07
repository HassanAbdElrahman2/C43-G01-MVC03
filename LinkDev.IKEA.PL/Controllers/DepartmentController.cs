using LinkDev.IKEA.BLL.Models.Deoartments;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.DAL.Entities.Departments;
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
        public IActionResult Details (int? id,string viewName= "Details")
        {
            if (!id.HasValue)
                return BadRequest();
            var department=_departmentService.GetDepartmentById(id.Value);

            if (department is null)
                return NotFound();
            var departmentViewModel = new DepartmentDetailsViewModel()
            {
                Id=id.Value,
                Code = department.Code,
                Name = department.Name,
                Description = department.Description??"",
                CreationDate = department.CreationDate,
                CreatedBy = department.CreatedBy,
                CreatedOn = department.CreatedOn,
                LastModifiedBy = department.LastModifiedBy,
                LastModifiedOn = department.LastModifiedOn



            };
            return View(viewName,departmentViewModel);
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
            if (!ModelState.IsValid) // Server-Side Validation
                return View(model);
            var massage = "Department Created Successfully";
            try
            {
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

        #region Update
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);

            if (department is null)
                return NotFound();
            var departmentViewModel = new UpdateDepartmentViewModel()
            {
                Id = department.Id,
                Code = department.Code,
                Name = department.Name,
                Description = department.Description ?? "",
                CreationDate = department.CreationDate


            };
            TempData["Id"] = id;
            return View(departmentViewModel);
        }

        [HttpPost ]
        public IActionResult Edit([FromRoute]int id,UpdateDepartmentViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            if (((int?)TempData["Id"]) !=id)
                return BadRequest();
            var message = "Department Update Successfully";
            try
            {
               
                var departmenttoUpdate = new UpdateDepartmentDto(id, model.Name, model.Description, model.Code, model.CreationDate);
                var updated = _departmentService.UpdateDepartment(departmenttoUpdate) > 0;
                if (!updated)
                    message = "Failed to update Department";
            }
            catch (Exception ex)
            {

                // 1. Log Exception in Database or External File (By Default Logging System In .Net or By Serialog Package)
                _logger.LogError(ex.Message, ex.StackTrace!.ToString());
                // 2. Set Massage
                message = "An Error Occurred, Please Try Again Later ";
            }
            TempData["Massage"] = message;
            return RedirectToAction(nameof(Index));

        }
        #endregion

        #region Delete
        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{

        //    return RedirectToAction(nameof(Details), new { id = id, viewName = "Delete" });
        //}

        [HttpPost] // POST: /Department/Delete/id
        public IActionResult Delete(int id)
        {
     

            var message = "Department Deleted Successfully";
            try
            {
                var deleted = _departmentService.DeleteDepartment(id) ;
                if (!deleted)
                    message = "Failed to Delete Department";
            }
            catch (Exception ex)
            {
                // 1. Log Exception in Database or External File (By Default Logging System in .NET or any other)
                _logger.LogError(ex.Message, ex.StackTrace!.ToString());
                // 2. Set Message
                message = "An Error Occurred, Please Try Again Later";
            }

            TempData["Message"] = message;
            return RedirectToAction(nameof(Index));
        }



        #endregion
    }


}
