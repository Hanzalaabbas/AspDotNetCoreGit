using AspMVCCoreGit.Models;
using AspMVCCoreGit.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;

namespace AspMVCCoreGit.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public IUserService UserService { get; }

        //private readonly UserManager<IdentityUser> _userManager;

        //public AccountRepository(UserManager<IdentityUser> userManager) 
        //{
        //    _userManager = userManager;
        //}
        public AccountRepository(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,IUserService userService,IEmailService emailService,IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _emailService = emailService;
            _configuration = configuration;
        }
        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
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
            if (result.Succeeded)
            {
               await GenerateEmailConfirmationTokenAsync(user);
            }
            return result;
        }
        public async Task GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendEmailConformationEmail(user, token);
            }
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
        public async Task<IdentityResult> CopnfirmEmailAsync(string uid,string token)
        {
          return await  _userManager.ConfirmEmailAsync(await _userManager.FindByIdAsync(uid),token);

        }
        private async Task SendEmailConformationEmail(ApplicationUser user,string token)
        {
            var appDomain = _configuration.GetSection("Application:AppDomain").Value;
            var confirmationLink = _configuration.GetSection("Application:EmailConfirmation").Value;
            UserEmailOptions emailOptions = new UserEmailOptions
            {
                ToEmail = new List<string>() { user.Email },
              PlaceHolders = new List<KeyValuePair<string, string>>() {
              new KeyValuePair<string,string>("{{UserName}}",user.FirstName),
                new KeyValuePair<string, string>("{{Link}}", string.Format(appDomain + confirmationLink,user.Id,token))
              }
            
            };
            await _emailService.SendEmailForConfirmation(emailOptions);
        }


    }
}
