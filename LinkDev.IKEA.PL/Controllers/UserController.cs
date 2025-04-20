using LinkDev.IKEA.DAL.Entities.IdentityModel;
using LinkDev.IKEA.PL.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Transport.NamedPipes;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.IKEA.PL.Controllers
{
    [Authorize]

   
    public class UserController : Controller
    {
        #region Services
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserController> _logger;

        public UserController(UserManager<ApplicationUser> userManager , ILogger<UserController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        } 
        #endregion

        #region Index
        public async Task<IActionResult> Index(string? SearchValue)
        {
            var UsersQuary = _userManager.Users.AsQueryable();
            if (!String.IsNullOrWhiteSpace(SearchValue))
                UsersQuary = _userManager.Users.Where(U => U.FirstName!.ToLower().Contains(SearchValue.ToLower()));
            var Users = await UsersQuary
                .Select(U => new UsersViewModel()
                {
                    FName = U.FirstName,
                    LName = U.LastName,
                    PhoneNumber = U.PhoneNumber,
                    Id = U.Id,
                    Email = U.Email

                }).ToListAsync();
            foreach (var user in Users)
            {
                if (user.Email is not null)
                {
                    var User = await _userManager.FindByEmailAsync(user.Email);
                    if (User is not null)
                        user.Roles = await _userManager.GetRolesAsync(User);
                }
            }

            return View(Users);
        }
        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details([FromRoute]string? Id)
        {
            if (Id is not null)
            {
                var user = _userManager.FindByIdAsync(Id).Result;
                if (user is not null)
                {
                    var UserViewModel = new UserDetailsViewModel()
                    {
                        Email = user.Email,
                        FName = user.FirstName,
                        LName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        Id = user.Id
                    };
                    return View(UserViewModel);
                }
                return NotFound();
            }
            return BadRequest();
        }
        #endregion

        #region Update

        [HttpGet]
        public IActionResult Edit([FromRoute]string? Id)
        {
            if (Id is not null) {
                var User = _userManager.FindByIdAsync(Id).Result;
                if(User is not null)
                {
                    var UserViewModel = new UserEditViewModel()
                    {
                        Id= User.Id,
                        FName = User.FirstName,
                        LName = User.LastName,
                        PhoneNumber = User.PhoneNumber
                    };
                    TempData["Id"]= User.Id;
                    return View(UserViewModel); 
                }
                return NotFound(); 
            }
            return BadRequest();
        }
        [HttpPost]
        public IActionResult Edit(UserEditViewModel userEdit)
        {
            if (!ModelState.IsValid)
                return View(userEdit);
            if (TempData["Id"]?.ToString() != userEdit.Id.ToString())
                return BadRequest();
                var User = _userManager.FindByIdAsync(userEdit.Id).Result;
                if (User is not null)
                {
                    var Massage = " User Update Successfully";
                    try
                    {
                        
                        User!.PhoneNumber = userEdit.PhoneNumber;
                        User!.FirstName = userEdit.FName;
                        User!.LastName = userEdit.LName;
                        var Result = _userManager.UpdateAsync(User).Result;
                        if (!Result.Succeeded)
                            Massage = "Failed to update User";

                    }
                    catch (Exception ex)
                    {

                        _logger.LogError(ex.Message, ex.StackTrace!.ToString());
                        Massage = "Failed to update User";

                    }
                    TempData["Massage"] = Massage;
                    return RedirectToAction(nameof(Index));

                }
            return NotFound();
            }

        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete([FromRoute]string? Id)
        {
            if(Id is not null)
            {
                var User = _userManager.FindByIdAsync(Id).Result;
                if (User is not null)
                {

                    var UserViewModel = new UserDetailsViewModel()
                    {
                        Email = User.Email,
                        FName = User.FirstName,
                        LName = User.LastName,
                        PhoneNumber = User.PhoneNumber,
                        Id = User.Id
                    };
                    return View(UserViewModel);
                }
                return NotFound();
            }
            return BadRequest();
        }
        [HttpPost]
        public IActionResult ConfiramDelete(string Id)
        {
            var User = _userManager.FindByIdAsync(Id).Result;
            var Massage = "User Deleted";
            try
            {
               
               if (User is not null)
                {
                    var Result = _userManager.DeleteAsync(User).Result;
                    if (!Result.Succeeded)
                        Massage = "User Not Deleted";
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message, ex.StackTrace!.ToString());
                Massage = "Failed to update User";

            }
            TempData["Massage"] = Massage;
            return RedirectToAction(nameof(Index));

        

    }

        #endregion
    }
}
