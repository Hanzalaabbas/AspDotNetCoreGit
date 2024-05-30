using AspMVCCoreGit.Data;
using AspMVCCoreGit.Models;
using AspMVCCoreGit.Repository;
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
//********************This Code is Used for Dependencies Code is Start********************
//**********************Transient(AddTransient<>)-A new instance of the service will be created every time it is requested.**********************
//**********************Scoped(AddScoped<>)-These are created onece per client request**********************
//**********************Singleton(AddSingleton<>)-This instance will be same  for the entire application **********************
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();
//********************This Code is Used for Dependencies Code is Start********************
//********************RuntimeCompilation code is End********************
//********************Configure Service code is Start********************
builder.Services.Configure< NewBookAlertConfig>(builder.Configuration.GetSection("NewBookAlert"));
//********************Configure Service code is End********************
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
app.UseRouting();

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
app.Run();
