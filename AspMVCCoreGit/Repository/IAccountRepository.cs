﻿using AspMVCCoreGit.Models;
using Microsoft.AspNetCore.Identity;

namespace AspMVCCoreGit.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(SignUpUserModel signUpUserModel);
        Task<SignInResult> PasswordSignInAsync(SignInModel signInModel);
        Task SignOutAsync();
    }
}