using LinkDev.IKEA.DAL.Entities.IdentityModel;
using LinkDev.IKEA.PL.ViewModels.Roles;
using LinkDev.IKEA.PL.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace LinkDev.IKEA.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _RoleManager;
        private readonly ILogger<RoleController> _logger;

        #region Services
        public RoleController(RoleManager<IdentityRole> RoleManager, ILogger<RoleController> logger)
        {
            _RoleManager = RoleManager;
            _logger = logger;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index(string? SearchValue)
        {

            var Roles = _RoleManager.Roles.AsQueryable();
            if (!String.IsNullOrWhiteSpace(SearchValue))
                Roles = _RoleManager.Roles.Where(R => R.Name!.ToLower().Contains(SearchValue.ToLower()));
            var RolesView = await Roles.Select(R => new RolesViewModel() { Id = R.Id, Name = R.Name! }).ToListAsync();

            return View(RolesView);
        }
        #endregion

        #region Create

        [HttpGet]
        public IActionResult Create() => View();
        [HttpPost]
        public IActionResult Create(RolesViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var Massage = "Role Is Created"; ;
            try
            {
                var Role = new IdentityRole() { Id = model.Id, Name = model.Name };

                var Result = _RoleManager.CreateAsync(Role).Result;
                if (!Result.Succeeded)
                    Massage = "Role Not Created";
            }
            catch (Exception ex)
            {

                Massage = "Role Not Created";
                _logger.LogError(ex.Message, ex.StackTrace!.ToString());

            }
            TempData["Massage"] = Massage;
            return RedirectToAction(nameof(Index));

        }



        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details([FromRoute] string? Id)
        {
            if (Id != null)
            {
                var Role = _RoleManager.FindByIdAsync(Id).Result;
                if (Role is not null)
                {
                    var RoleView = new RolesViewModel() { Id = Role.Id.ToString(), Name = Role.Name! };
                    return View(RoleView);

                }
                return NotFound();
            }
            return BadRequest();
        }
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Edit([FromRoute] string? Id)
        {
            if (Id is not null)
            {
                var Role = _RoleManager.FindByIdAsync(Id).Result;
                if (Role is not null)
                {
                    var RoleViewModel = new RolesViewModel()
                    {
                        Id = Role.Id,
                        Name = Role.Name!,
                    };
                    TempData["Id"] = Role.Id;
                    return View(RoleViewModel);
                }
                return NotFound();
            }
            return BadRequest();
        }
        [HttpPost]
        public IActionResult Edit(RolesViewModel RoleEdit)
        {
            if (!ModelState.IsValid)
                return View(RoleEdit);
            if (TempData["Id"]?.ToString() != RoleEdit.Id.ToString())
                return BadRequest();
            var Role = _RoleManager.FindByIdAsync(RoleEdit.Id).Result;
            if (Role is not null)
            {
                var Massage = " Role Update Successfully";
                try
                {

                    Role.Name = RoleEdit.Name;
                    var Result = _RoleManager.UpdateAsync(Role).Result;
                    if (!Result.Succeeded)
                        Massage = "Failed to update Role";

                }
                catch (Exception ex)
                {

                    _logger.LogError(ex.Message, ex.StackTrace!.ToString());
                    Massage = "Failed to update Role";

                }
                TempData["Massage"] = Massage;
                return RedirectToAction(nameof(Index));

            }
            return NotFound();
        }


        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete([FromRoute] string? Id)
        {
            if (Id is not null)
            {
                var Role = _RoleManager.FindByIdAsync(Id).Result;
                if (Role is not null)
                {

                    var RoleViewModel = new RolesViewModel()
                    {
                        
                        Name = Role.Name!,
                        
                        Id = Role.Id
                    };
                    return View(RoleViewModel);
                }
                return NotFound();
            }
            return BadRequest();
        }
        [HttpPost]
        public IActionResult ConfiramDelete(string Id)
        {
            var Role = _RoleManager.FindByIdAsync(Id).Result;
            var Massage = "Role Deleted";
            try
            {

                if (Role is not null)
                {
                    var Result = _RoleManager.DeleteAsync(Role).Result;
                    if (!Result.Succeeded)
                        Massage = "Role Not Deleted";
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message, ex.StackTrace!.ToString());
                Massage = "Failed to Delete Role";

            }
            TempData["Massage"] = Massage;
            return RedirectToAction(nameof(Index));



        }

        #endregion
    } 
}
