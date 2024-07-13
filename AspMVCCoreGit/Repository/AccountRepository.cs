using AspMVCCoreGit.Models;
using AspMVCCoreGit.Services;
using Microsoft.AspNetCore.Identity;

namespace AspMVCCoreGit.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserService _userService;

        public IUserService UserService { get; }

        //private readonly UserManager<IdentityUser> _userManager;

        //public AccountRepository(UserManager<IdentityUser> userManager) 
        //{
        //    _userManager = userManager;
        //}
        public AccountRepository(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
        }
        public async Task<IdentityResult> CreateUserAsync(SignUpUserModel signUpUserModel)
        {
            //var user = new IdentityUser 
            //{ 
            //    Email = signUpUserModel.Email,
            //    UserName = signUpUserModel.Email 
            //};
            var user = new ApplicationUser
            {
                FirstName = signUpUserModel.FirstName,
                LastName = signUpUserModel.LastName,
                Email = signUpUserModel.Email,
                UserName = signUpUserModel.Email
            };
            var result = await  _userManager.CreateAsync(user,signUpUserModel.Password);
            return result;
        }
        public async Task<SignInResult> PasswordSignInAsync(SignInModel signInModel)
        {
            var result = await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, signInModel.RememberMe, false);
            return result;
        }
        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel changePasswordModel)
        {
            var userId = _userService.GetUserId();
            var user =await _userManager.FindByIdAsync(userId);
            return await _userManager.ChangePasswordAsync(user, changePasswordModel.CurrentPassword, changePasswordModel.NewPassword); 
           
        }


    }
}
