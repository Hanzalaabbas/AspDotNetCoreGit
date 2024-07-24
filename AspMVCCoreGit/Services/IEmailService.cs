using AspMVCCoreGit.Models;

namespace AspMVCCoreGit.Services
{
    public interface IEmailService
    {
        Task SendTestEmail(UserEmailOptions userEmailOptions);
    }
}