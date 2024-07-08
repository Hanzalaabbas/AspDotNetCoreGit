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
        public async Task<IActionResult>  LogIn(SignInModel signInModel)
        {
            if (ModelState.IsValid) {
            
            var result =await _accountRepository.PasswordSignInAsync(signInModel);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index","Home");
                }
                ModelState.AddModelError("", "Invalid Credentials");
            }
            return View();
        }

    }
}
