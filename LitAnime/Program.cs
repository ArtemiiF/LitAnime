using LitAnime.Services;
using LitAnime.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentity<User, IdentityRole>(opts =>
{
    opts.User.RequireUniqueEmail = true;
    opts.Password.RequiredLength = 6;
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequireDigit = false;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie.Name = "LitAnimeCookie";
    options.LoginPath = "/account/login";
    options.AccessDeniedPath = "/account/accessdenied";
});

builder.Services.AddDbContext<AppDbContext>(x => x.UseNpgsql(Config.ConnectionString));

builder.Services.AddAuthorization(x=>
{
    x.AddPolicy("AdminArea", policy => { policy.RequireRole("admin"); });
    x.AddPolicy("ModeratorArea", policy => { policy.RequireRole("moderator"); });
    x.AddPolicy("UserArea", policy => { policy.RequireRole("user"); });
});

builder.Services.AddMvc(x =>
{
    x.Conventions.Add(new AdminAreaAuth("Admin", "AdminArea"));
    x.Conventions.Add(new AdminAreaAuth("Moderator", "ModeratorArea"));
    x.Conventions.Add(new AdminAreaAuth("User", "UserArea"));
}).AddSessionStateTempDataProvider();

builder.Configuration.Bind("Project", new Config());

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseStaticFiles();
app.UseFileServer(new FileServerOptions
{
    FileProvider = new PhysicalFileProvider(Config.ImagePath),
    RequestPath = new PathString("/DBImage"),
    EnableDirectoryBrowsing = false
});

app.UseRouting();

app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(name: "admin", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
        endpoints.MapControllerRoute(name: "moderator", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
        endpoints.MapControllerRoute(name: "user", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
        endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
        
    }
);


app.Run();