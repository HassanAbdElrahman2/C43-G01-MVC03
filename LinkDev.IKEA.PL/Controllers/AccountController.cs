using LinkDev.IKEA.DAL.Entities.IdentityModel;
using LinkDev.IKEA.PL.ViewModels.Accounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager) : Controller
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
            if (!ModelState.IsValid)
                return View(viewModel);

            var model = new ApplicationUser()
            {
                UserName = viewModel.UserName,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
            };
            var Result = _userManager.CreateAsync(model, viewModel.Password).Result;
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

        #region Login
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user=await _userManager.FindByEmailAsync(loginViewModel.Email);
                if (user is not null)
                {
                    var PassIsExist = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                    if (PassIsExist)
                    {
                        var Result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false);
                            if (Result.Succeeded)
                            return RedirectToAction("Index","Home");
                    }
                    else
                        ModelState.AddModelError(String.Empty, "Email or Password not true");
                }
                else
                    ModelState.AddModelError(String.Empty, "Email or Password not true");
            }
         
             return View(loginViewModel);
           
        }
        #endregion

        #region SignOut
        [HttpGet]
        public new async Task<IActionResult> SignOut()
        {
              await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        #endregion
    }
}