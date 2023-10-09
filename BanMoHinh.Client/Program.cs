using BanMoHinh.Client.IServices;
using BanMoHinh.Client.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// add Dependency Injection
builder.Services.AddHttpClient();
builder.Services.AddScoped<IAuthenticationService,AuthenticationService>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
});
// add authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
   .AddCookie(options =>
   {
       options.LoginPath = "/Authentication/Login";
       options.LogoutPath = "/Authentication/LogOut";
       options.Cookie.HttpOnly = true;
       options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
       options.Cookie.SameSite = SameSiteMode.None;
       options.Cookie.Name = "Cookie_Cua_Trung";
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

app.UseAuthentication(); // Kích hoạt xác thực
app.UseAuthorization(); // Kích hoạt quyền truy cập

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authentication}/{action=DemoLogin}/{id?}");
});

app.Run();
