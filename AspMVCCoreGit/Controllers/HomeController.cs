using AspMVCCoreGit.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Dynamic;

namespace AspMVCCoreGit.Controllers
{
    //This is Token Replacment Code is ***************************** Start
    [Route("[controller]/[action]")]
    //This is Token Replacment Code is ***************************** End
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        //****************Code Start for [ViewData] Attribut****************
        [ViewData]
        public string? Title { get; set; }
        //****************Code End for [ViewData] Attribut****************
        [Route("~/")]
        //[Route("home/index")] *****************If we want to change in future controller or action method name then its change route to for this reslove this issue we can use "Token Replacment" 
        public IActionResult Index()
        {

            Title = "Home from Controller to Layout USing ViewData Attributes";
            return View();
        }
        //[Route("Priv-acy/{id?}/{FullName?}")]
       // [HttpGet("Priv-acy/{id?}/{FullName?}", Name = "Privacy" , Order =1)]
        public IActionResult Privacy(int id,string FullName)
        {
            //****************Code Start for Dynamic View****************
            //dynamic data = new System.Dynamic.ExpandoObject(); 
            //data.Title = Title;
            //data.Name = Title;
            //return View(data);
            //****************Code Start for Dynamic View****************
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
