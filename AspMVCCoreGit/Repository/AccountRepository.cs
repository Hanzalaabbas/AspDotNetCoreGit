using AspMVCCoreGit.Models;
using Microsoft.AspNetCore.Identity;

namespace AspMVCCoreGit.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly UserManager<IdentityUser> _userManager;

        //public AccountRepository(UserManager<IdentityUser> userManager) 
        //{
        //    _userManager = userManager;
        //}
        public AccountRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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
            var result =await  _userManager.CreateAsync(user,signUpUserModel.Password);
            return result;
        }
    }
}
