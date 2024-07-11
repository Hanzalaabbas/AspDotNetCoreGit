
namespace AspMVCCoreGit.Services
{
    public interface IUserService
    {
        IHttpContextAccessor HttpContextAccessor { get; }

        string GetUserId();
        bool IsAuthenticated();
    }
}