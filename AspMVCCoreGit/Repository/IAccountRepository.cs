using AspMVCCoreGit.Models;
using Microsoft.AspNetCore.Identity;

namespace AspMVCCoreGit.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(SignUpUserModel signUpUserModel);
    }
}