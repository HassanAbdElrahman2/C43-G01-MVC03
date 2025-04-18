using LinkDev.IKEA.DAL.Entities.IdentityModel;
using LinkDev.IKEA.PL.ViewModels.Accounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LinkDev.IKEA.PL.Utility;


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

        #region Forget Password
        [HttpGet]
        public IActionResult ForgetPassword() => View();
        [HttpPost] 
        public  IActionResult ForgetPassword(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
               
                
                var user =  _userManager.FindByEmailAsync(model.Email).Result;
                if (user is not null)
                {
                    var Token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
                    var Link = Url.Action(nameof(ResetPassword), "Account"
                        , new { Email = model.Email, Token }, Request.Scheme);

                    var Email = new Email() { To = model.Email, Subject = "Reset Password", Body = Link??"Error In Opration" };
                    EmailSettings.SendEmail(Email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
            }
            ModelState.AddModelError(String.Empty, "Opration Invalid");
            return  View(model);

        }
        [HttpGet]
        public IActionResult CheckYourInbox() => View();
        [HttpGet]
        public IActionResult ResetPassword(string Email, string Token) 
        {
            TempData["email"] = Email;
            TempData["token"]= Token;
            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(RestPasswordViewModel restPassword) 
        {
            if (ModelState.IsValid) 
            {
                var Email = TempData["email"] as string ?? String.Empty;
                var Token = TempData["token"] as string ?? String.Empty;
                var user = _userManager.FindByEmailAsync(Email).Result;
                if (user is not null)
                {
                    var Result = _userManager.ResetPasswordAsync(user, Token, restPassword.NewPassword).Result;
                    if (Result.Succeeded)
                        return RedirectToAction(nameof(Login));
                    else 
                        foreach (var error in Result.Errors)
                        {
                            ModelState.AddModelError(String.Empty, error.Description);

                        }
                }
            }
            return View(nameof(ResetPassword));
        }
        #endregion
    }
}