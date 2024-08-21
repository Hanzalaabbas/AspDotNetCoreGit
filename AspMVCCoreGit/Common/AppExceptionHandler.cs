using Microsoft.AspNetCore.Diagnostics;

namespace AspMVCCoreGit.Common
{
    public class AppExceptionHandler : IExceptionHandler
    {
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
