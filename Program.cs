using Kampusum.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

builder.Services.AddAuthentication("Cookies")
.AddCookie("Cookies", options =>
{
    options.LoginPath = "/Login/Login";
    options.Events = new CookieAuthenticationEvents
    {
        OnRedirectToLogin = context =>
        {
            var path = context.Request.Path.ToString().ToLower();

            if (path.Contains("/admin") || path.Contains("/news/create") || path.Contains("/news/delete") || path.Contains("/events/create") || path.Contains("/events/delete")
            || path.Contains("/announcement/create") || path.Contains("/announcement/delete"))
            {
                context.Response.Redirect("/Admin/Login?ReturnUrl=" + Uri.EscapeDataString(context.Request.Path));
            }
            else
            {
                context.Response.Redirect("/Login/Login?ReturnUrl=" + Uri.EscapeDataString(context.Request.Path));
            }

            return Task.CompletedTask;
        }
    };
});

builder.Services.AddScoped<EmailService>(sp => new EmailService(
    smtpHost: builder.Configuration["EmailSettings:SmtpHost"],
    smtpPort: int.Parse(builder.Configuration["EmailSettings:SmtpPort"]),
    emailFrom: builder.Configuration["EmailSettings:EmailFrom"],
    emailPassword: builder.Configuration["EmailSettings:EmailPassword"]
));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Starter}/{id?}");

app.Run();
