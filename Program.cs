using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity.UI.Services;
using WebApp.Services;

var builder = WebApplication.CreateBuilder(args);




// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(p =>
    {
        p.LoginPath = "/auth/login";
        p.LogoutPath = "/auth/logout";
        p.AccessDeniedPath = "/auth/denied";
    })
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"] ?? throw new Exception("Not found ClientId");
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? throw new Exception("Not found ClientSecret");
    })
    .AddFacebook(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Facebook:ClientId"]
            ?? throw new Exception("Not found ClientId");
        options.ClientSecret = builder.Configuration["Authentication:Facebook:ClientSecret"]
            ?? throw new Exception("Not found ClientSecret");
    });

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddScoped<IEmailSender, EmailSender>();


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

app.MapControllerRoute(name: "admin", pattern: "{area:exists}/{controller=home}/{action=index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
