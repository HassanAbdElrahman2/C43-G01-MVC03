using LinkDev.IKEA.DAL.Entities.IdentityModel;
using LinkDev.IKEA.PL.ViewModels.Accounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager) : Controller
    {
        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel viewModel)
        {
            if(!ModelState.IsValid)
                return View(viewModel);

            var model = new ApplicationUser() 
            {
                UserName= viewModel.UserName,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
            };
            var Result=_userManager.CreateAsync(model,viewModel.Password).Result;
            if (Result.Succeeded)
               return RedirectToAction("Login");
            else
            {
                foreach (var error in Result.Errors)
                {
                    ModelState.AddModelError(String.Empty, error.Description);
                }
                return View(viewModel);
            }
        } 
        #endregion
    }
}
