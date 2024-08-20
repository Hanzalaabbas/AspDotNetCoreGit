using Microsoft.AspNetCore.Mvc;

namespace AspMVCCoreGit.Areas.Financial.Controllers
{
    [Area("financial")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
