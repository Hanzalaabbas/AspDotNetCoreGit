using Microsoft.Extensions.FileProviders;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. 
//********************IF we want to use for asp.net core project then add this method AddControllersWithViews(), ********************
//********************IF you want mvc core and Razor Pages then call this methodAddMVC()********************
//********************If you want to work on Razor Pages peoject then you call this method AddRazorPages()********************
//********************If you want to work on WEB API Project then you call this method AddController()********************
builder.Services.AddControllersWithViews();

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
//app.UseStaticFiles(new StaticFileOptions()
//{
//    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"MyStaticFile")),
//    RequestPath = "/MyStaticFile"
//});
//********************UseStaticFiles if we create outside wwwroot folder so for this use Code is End********************
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
