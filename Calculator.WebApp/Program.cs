using Calculator.WebApp.Features.Shared;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Calculator.WebApp;

public abstract class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllersWithViews();
        builder.Services.Configure<RazorViewEngineOptions>(options =>
        {
            options.ViewLocationFormats.Clear();
            options.ViewLocationFormats.Add("~/Features");
            options.ViewLocationFormats.Add("~/Features/{1}/{0}.cshtml");
            options.ViewLocationFormats.Add("~/Features/Shared/{0}.cshtml");
        });
        builder.Services.AddSessionServices(); // what we need for Calculator
        builder.Services.AddAntiforgery();
        // build the app 
        var app = builder.Build();
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseSession(); 
        app.UseRouting();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.Run();
    }
}