using AspMVCCoreGit.Models;
using AspMVCCoreGit.Repository;
using AspMVCCoreGit.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Dynamic;

namespace AspMVCCoreGit.Controllers
{
    //This is Token Replacment Code is ***************************** Start
    // [Route("[controller]/[action]")]
    //This is Token Replacment Code is ***************************** End
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IOptions<NewBookAlertConfig> newBookAlertConfig;
        private readonly IOptionsSnapshot<NewBookAlertConfig> newBookAlertConfigSnapshot;
        private readonly NewBookAlertConfig _newBookAlertConfig;//Read configuration using option pattern (IOptions) from appsettings 
        private readonly NewBookAlertConfig _newBookAlertConfigSnapshot;//Read configuration using option pattern (IOptions) from appsettings 
        private readonly IMessageRepository _messageRepository;
        private readonly IMessageRepository _newBookAlertConfigMonitor;//Reload configuration in singleton service | IOptionsMonitor |
        private readonly IOptionsSnapshot<NewBookAlertConfig> thiredPartyBookConfig;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;//This is use for send email via Identity Framework
        private readonly NewBookAlertConfig _ThiredPartyBookConfig;//Read configuration using Named options in asp.net core from appsettings 

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IOptions<NewBookAlertConfig> newBookAlertConfig, IOptionsSnapshot<NewBookAlertConfig> newBookAlertConfigSnapshot, IMessageRepository messageRepository, IMessageRepository newBookAlertConfigMonitor, IOptionsSnapshot<NewBookAlertConfig> thiredPartyBookConfig,IUserService userService,IEmailService emailService)
        {
            _logger = logger;
            this._configuration = configuration;
            this.newBookAlertConfig = newBookAlertConfig;
            this.newBookAlertConfigSnapshot = newBookAlertConfigSnapshot;
            _newBookAlertConfig = newBookAlertConfig.Value;//Read configuration using option pattern (IOptions) from appsettings 
            _newBookAlertConfigSnapshot = newBookAlertConfigSnapshot.Value;//Read configuration using option pattern (IOptionsSnapshot) from appsettings 
            _messageRepository = messageRepository;
            _newBookAlertConfigMonitor = newBookAlertConfigMonitor;//Reload configuration in singleton service | IOptionsMonitor |
            this.thiredPartyBookConfig = thiredPartyBookConfig;
            _userService = userService;
            _emailService = emailService;
            _ThiredPartyBookConfig = thiredPartyBookConfig.Get("ThiredPartyBook");//Read configuration using Named options in asp.net core from appsettings 
        }
        //****************Code Start for [ViewData] Attribut****************
        [ViewData]
        public string? Title { get; set; }
        //****************Code End for [ViewData] Attribut****************
        [Route("~/")]
        //[Route("home/index")] *****************If we want to change in future controller or action method name then its change route to for this reslove this issue we can use "Token Replacment" 
        public async Task<IActionResult> Index()
        {
           // UserEmailOptions emailOptions = new UserEmailOptions
           // {
           //     ToEmail = new List<string>() { "hanzlaabbas88@gmail.com" },
           //     PlaceHolders = new List<KeyValuePair<string,string>>() {
           //     new KeyValuePair<string,string>("{{UserName}}","Hanzala")}
           // };
           //await _emailService.SendTestEmail(emailOptions);
            var userId= _userService.GetUserId();//this way we can get  UserID anywhere in controller and service  
            var isLoggdIn = _userService.IsAuthenticated();
            bool isDisplaySnapshot = _newBookAlertConfigSnapshot.DisplayNewBookAlert;//Read configuration using option pattern (IOptionsSnapshot) from appsettings 
            bool isDisplay = _newBookAlertConfig.DisplayNewBookAlert;//Read configuration using option pattern (IOptions) from appsettings 
            bool isDisplay1 = _ThiredPartyBookConfig.DisplayNewBookAlert;//Read configuration using Named options in asp.net core from appsettings 
            //var newBookAlert = new NewBookAlertConfig();// this way we can get value from appsetting using Bind method  
            //_configuration.Bind("NewBookAlert", newBookAlert);
            //bool isDisplay = newBookAlert.DisplayNewBookAlert;
            //string DisplayName = newBookAlert.BookName;
            var value = _messageRepository.GetName();
            var value2 = _newBookAlertConfigMonitor.GetName1();



            var newBook = _configuration.GetSection("NewBookAlert").GetValue<bool>("DisplayNewBookAlert");
            //var resultbool =_configuration.GetValue<bool>("NewBookAlert:DisplayNewBookAlert");
            var resultstring =_configuration.GetValue<string>("NewBookAlert:BookName");
            var result = _configuration["AppName"];
            var key1 = _configuration["InfoBoj.key1"];
            var key2 = _configuration["InfoBoj.key2"];
            var key3 = _configuration["InfoBoj.key3:key3obj1"];
            Title = "Home from Controller to Layout USing ViewData Attributes";
            return View();
        }
        //[Route("Priv-acy/{id?}/{FullName?}")]
        // [HttpGet("Priv-acy/{id?}/{FullName?}", Name = "Privacy" , Order =1)]
        [Route("Priv-acy/{FullName:alpha:minlength(5)}")]// *************Added here alpha constraint because  its only take alphabet****************
        public IActionResult Privacy(string FullName)
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
        [Authorize(Roles ="Admin")]
        [Route("~/contact-us")]
        public ViewResult ContactUs()
        {
            return View();
        }
     
    }
}
