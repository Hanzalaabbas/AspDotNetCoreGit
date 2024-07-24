using AspMVCCoreGit.Models;
using AspMVCCoreGit.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AspMVCCoreGit.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [HttpGet]
        [Route("signup")]
        public IActionResult SignUp()
        {
            return View();
        }
        [Route("signup")]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpUserModel signUpUserModel)
        {
            if(ModelState.IsValid)
            {
                var result = await _accountRepository.CreateUserAsync(signUpUserModel);
                if (!result.Succeeded) 
                { 
                foreach (var item in result.Errors) 
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                return View(signUpUserModel);
                }
                ModelState.Clear();
            }
            return View();
        }
        [HttpGet]
        [Route("Login")]
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult>  LogIn(SignInModel signInModel,string returnUrl)
        {
            if (ModelState.IsValid) {
            
            var result =await _accountRepository.PasswordSignInAsync(signInModel);
                if(result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    return RedirectToAction("Index","Home");
                }
                if(result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Not Allowed to Login");
                }
                else
                { 
                ModelState.AddModelError("", "Invalid Credentials");
                }
            }
            return View();
        }

        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountRepository.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        
        [Route("Change-Password")]
        public IActionResult ChangePassword()
        {
           
            return View();
        }
        
        [HttpPost("Change-Password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            if(ModelState.IsValid)
            {
                ViewBag.IsSuccess = true;
                var result =await _accountRepository.ChangePasswordAsync(changePasswordModel);
                if(result.Succeeded)
                {
                    ModelState.Clear();
                    return View();
                }
            }

            return View(changePasswordModel);
        }

    }
}
