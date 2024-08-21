using AspMVCCoreGit.Models;
using AspMVCCoreGit.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
        public async Task<IActionResult> SignUp(SignUpUserModel userModel)
        {
            if(ModelState.IsValid)
            {
                var result = await _accountRepository.CreateUserAsync(userModel);
                if (!result.Succeeded) 
                { 
                foreach (var item in result.Errors) 
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                return View(userModel);
                }
                ModelState.Clear();
                return RedirectToAction("ConfirmEmail", new {email = userModel.Email});
            }
            return View(userModel);
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
                else if(result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Account blocked. Try after  some time.");
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
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string uid,string token,string email)
        {
            EmailConfirmModel model = new EmailConfirmModel()
            {
                Email = email,
            };
            if(!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(token))
            {
                token = token.Replace(' ', '+');
              var result = await  _accountRepository.CopnfirmEmailAsync(uid, token);
                if(result.Succeeded)
                {
                    //ViewBag.IsSuccess = true;
                    model.EmailVerified = true;
                }
            }
            return View(model);
        }
        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(EmailConfirmModel model)
        {
            var user = await _accountRepository.GetUserByEmailAsync(model.Email);
            if(user != null)
            {
                if(user.EmailConfirmed)
                {
                    model.EmailVerified = true;
                    return View(model);
                }
                await _accountRepository.GenerateEmailConfirmationTokenAsync(user);
                model.EmailSent = true;
                ModelState.Clear();
            }
            else
            {
                ModelState.AddModelError("", "Somthing went wrong.");
            }
            return View(model);
        }
        [AllowAnonymous,HttpGet("forgot-password")]
        public ActionResult FogotPassword()
        {

            return View();
        }
        [AllowAnonymous, HttpPost("forgot-password")]
        public async Task<ActionResult> FogotPassword(ForgotPasswordModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await _accountRepository.GetUserByEmailAsync(model.Email); 
                if(user != null)
                {
                 await  _accountRepository.GenerateForgotPasswordTokenAsync(user);
                }
                ModelState.Clear();
                model.EmailSent = true;
            }

            return View(model);
        }
        [AllowAnonymous, HttpGet("reset-password")]
        public ActionResult ResetPassword(string uid,string token)
        {
            ResetPasswordModel resetPasswordModel = new ResetPasswordModel()
            {
                Token =token,
                UserId=uid
            };
            return View(resetPasswordModel);
        }
        [AllowAnonymous, HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
               model.Token= model.Token.Replace(' ', '+');
                var result =await _accountRepository.RestPasswordAsync(model);
                if (result.Succeeded)
                {
                    ModelState.Clear();
                    model.IsSuccess = true;
                    return View(model);
                }
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);   
                }
               
            }

            return View(model);
        }
    }
}
