using Kampusum.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Login/Login";
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
