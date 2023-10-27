using MeetingApp.Application;
using MeetingApp.Persistence;
using MeetingApp.UI.Tools;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddCookie(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    opt.AccessDeniedPath = "/accesdenied";
    opt.LoginPath = "/Auth/Login";
    opt.LogoutPath = "/Auth/Logout";
    opt.Cookie.SameSite = SameSiteMode.Strict;
    opt.Cookie.HttpOnly = true;
    opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    opt.Cookie.Name = "MeetingAppCookie";
});


builder.Services.AddControllersWithViews();
builder.Services.AddApplicationServices();

builder.Services.AddPersistenceServices();
builder.Services.AddApplicationServices();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "/",
        defaults: new { controller = "Meetings", action = "AllMeetings" }
    );
    endpoints.MapControllerRoute(
        name: "meetings",
        pattern: "Meetings/{action}/{id?}",
        defaults: new { controller = "Meetings" }
    );
    endpoints.MapDefaultControllerRoute();
});
app.Run();
