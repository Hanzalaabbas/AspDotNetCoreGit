using AspMVCCoreGit.Common;
using AspMVCCoreGit.Data;
using AspMVCCoreGit.Helpers;
using AspMVCCoreGit.Models;
using AspMVCCoreGit.Repository;
using AspMVCCoreGit.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container. 
//********************IF we want to use for asp.net core project then add this method AddControllersWithViews(), ********************
//********************IF you want mvc core and Razor Pages then call this methodAddMVC()********************
//********************If you want to work on Razor Pages peoject then you call this method AddRazorPages()********************
//********************If you want to work on WEB API Project then you call this method AddController()********************
builder.Services.AddControllersWithViews();
//********************RuntimeCompilation code is Start********************
#if DEBUG
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
//builder.Services.AddRazorPages().AddRazorRuntimeCompilation().AddViewOptions(
//    option => option.HtmlHelperOptions.ClientValidationEnabled = false);
#endif
//******************** Using this service we can add Connection String code is Start********************
//builder.Services.AddDbContext<BookStoreContext>(options => options.UseSqlServer("Server=.,Database=BookStore;Integrated Security=True;"));
//builder.Services.AddDbContext<BookStoreContext>(options => options.UseSqlServer("Data Source=.;Database=BookStore;Integrated Security=True;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));
builder.Services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//******************** Using this service we can add Connection String code is End********************
//******************** Using this service we can add AddIdentity with database code is Start********************
//builder.Services.AddIdentity<IdentityUser,IdentityRole>().AddEntityFrameworkStores<BookStoreContext>();
//******************** Using this service we can add AddIdentity with database code is End********************
//******************** Using this service we can add new column instead of  IdentityUser  class  code is Start********************
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<BookStoreContext>().AddDefaultTokenProviders();
//******************** Using this service we can add new column instead of  IdentityUser  class  code is End********************
//******************** Using this service we can configure the password complexity in Identity core  code is Start********************
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 5;
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.SignIn.RequireConfirmedEmail = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
    options.Lockout.MaxFailedAccessAttempts = 3;//by default 5 attemptes is for wrong password.
    
});
//******************** Using this service we can configure the password complexity in Identity core  code is End********************
//******************** Using this service we can configure Token lifespan in Identity core  code is Start********************
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromMinutes(5);
});
//******************** Using this service we can configure Token lifespan in Identity core  code is End********************




//********************This Code is Used for add service for exception handeli Code is Start********************
//builder.Services.AddExceptionHandler<AppExceptionHandler>();   
//********************This Code is Used for add service for exception handeli Code is End********************

//********************This Code is Used for Dependencies Code is Start********************
//**********************Transient(AddTransient<>)-A new instance of the service will be created every time it is requested.**********************
//**********************Scoped(AddScoped<>)-These are created onece per client request**********************
//**********************Singleton(AddSingleton<>)-This instance will be same  for the entire application **********************
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();
builder.Services.AddSingleton<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
//********************This Code is Used for Dependencies Code is Start********************
//********************RuntimeCompilation code is End********************
//********************UserClaimsPrincipalFactory service for store first and lastname store in claims code is Start********************
builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();
//********************UserClaimsPrincipalFactory service for store first and lastname store in claims code is End********************
//********************EmailService service is configure because this service is send email code is Start********************
builder.Services.AddScoped<IEmailService, EmailService>();
//********************EmailService service is configure because this service is send email code is End********************
//********************Configure Service code is Start********************
builder.Services.Configure< NewBookAlertConfig>("InternalBook",builder.Configuration.GetSection("NewBookAlert"));
builder.Services.Configure<NewBookAlertConfig>("ThiredPartyBook", builder.Configuration.GetSection("ThiredPartyBook"));
//********************Configure Service code is End********************
//********************using  Redirect user to login page (custom login url) Service for Authorization code is Start********************
var loginPath = builder.Configuration.GetSection("Application:LoginPath").Value;
builder.Services.ConfigureApplicationCookie(builder =>
{
    builder.LoginPath = loginPath; //"/login";
});
//********************using  Redirect user to login page (custom login url) Service for Authorization code is End********************
//********************using this service  Get logged-in user id in controller and sevices  code is Start********************
builder.Services.AddScoped<IUserService, UserService>();
//********************using this service  Get logged-in user id in controller and sevices  code is End********************
//********************using this service  we can configureSMTP   code is Start********************
builder.Services.Configure<SMTPConfigModel>(builder.Configuration.GetSection("SMTPConfig"));
//********************using this service  we can configureSMTP   code is  End********************
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
//********************Environment Variable Code is Start********************
//app.Use(async (context, next) =>
//{
//    if(app.Environment.IsDevelopment())
//    {
//        await context.Response.WriteAsync("This is Developement Environment");
//    }
//    else if (app.Environment.IsStaging())
//    {
//        await context.Response.WriteAsync("This is Staging Environment");
//    }
//    else if(app.Environment.IsProduction()) {
//        await context.Response.WriteAsync("This is Producation Environment");
//    }
//    else
//    await context.Response.WriteAsync(app.Environment.EnvironmentName);
//     await next();

//});
//********************Environment Variable Code is End********************
//********************Endpoint Code is Start********************
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapGet("/", async context =>
//    {
//        await context.Response.WriteAsync("Hello World!");
//    });
//});
//********************Endpoint Code is End********************
app.UseStaticFiles();
//********************UseStaticFiles if we create outside wwwroot folder so for this use Code is Start********************
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "MyStaticFile")),
    RequestPath = "/MyStaticFile"
});
//********************UseStaticFiles if we create outside wwwroot folder so for this use Code is End********************
//app.UseExceptionHandler(_ => { });
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
//app.MapDefaultControllerRoute();
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");
//Conventional Routing code is ***************************** start
//app.MapControllerRoute(
//    name: "Privacy",
//    pattern: "Priv-acy/{id?}",
//    defaults: new {controller="Home" , action= "Privacy" });
//Conventional Routing code is ***************************** End

app.MapControllerRoute(
            name: "MyArea",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.Run();
