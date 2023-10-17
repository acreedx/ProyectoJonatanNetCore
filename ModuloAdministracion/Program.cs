using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using ModuloAdministracion.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.AccessDeniedPath = "/Site/Index";
        option.LoginPath = "/Site/InicioDeSesion";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });
builder.Services.AddDbContext<DBFARMACIAContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql")
));
builder.Services.AddSingleton<IWebHostEnvironment>(builder.Environment);
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Site}/{action=Index}/{id?}");

app.Run();
