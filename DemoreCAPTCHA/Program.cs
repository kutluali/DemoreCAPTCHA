using DemoreCAPTCHA.DAL.Context;
using DemoreCAPTCHA.DAL.Entities;
using DemoreCAPTCHA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MySite.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

       
        builder.Services.AddDbContext<reCAPTCHAContext>(options =>
            options.UseSqlServer(connectionString));

     
        builder.Services.AddIdentity<AppUser, AppRole>(options =>
        {
            
        })
        .AddEntityFrameworkStores<reCAPTCHAContext>()
        .AddDefaultTokenProviders();


        //builder.Services.AddScoped<GoogleReCaptchaService>();
        //builder.Services.Configure<GoogleReCAPTCHA>(builder.Configuration.GetSection("GoogleReCAPTCHA"));

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.ConfigureApplicationCookie(x =>
        {
            x.LoginPath = "/Account/Login";
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}