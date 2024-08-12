using AspMVCCoreGit.Models;

namespace AspMVCCoreGit.Services
{
    public interface IEmailService
    {
        Task SendEmailForConfirmation(UserEmailOptions emailOptions);
        Task SendTestEmail(UserEmailOptions userEmailOptions);
        Task SendEmailForForgotPassword(UserEmailOptions userEmailOptions);
    }
}