using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MintLynk.Application.Interfaces;
using MintLynk.Application.Services;
using MintLynk.Domain.Interfaces;
using MintLynk.Infrastructure.Data;
using MintLynk.Infrastructure.Identity;
using MintLynk.Infrastructure.Repositories;
using System;

var builder = WebApplication.CreateBuilder(args);

// --- ADDED FOR RAILWAY PORT BINDING ---
// Railway gives you a PORT variable. We must listen to 0.0.0.0 on that port.
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
// --------------------------------------

// Add database context
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("AppConnection")));

// Add Identity services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    options.SignIn.RequireConfirmedAccount = true;
    options.SignIn.RequireConfirmedEmail = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

// Register application services
builder.Services.AddScoped(typeof(ISmartLinkRepository), typeof(SmartLinkRepository));
builder.Services.AddScoped(typeof(ILinkHistoryRepository), typeof(LinkHistoryRepository));
builder.Services.AddScoped(typeof(INotificationCenterRepository), typeof(NotificationCenterRepository));
builder.Services.AddScoped(typeof(IMiniPageRepository), typeof(MiniPageRepository));

builder.Services.AddScoped(typeof(IUrlShorteningService), typeof(UrlShorteningService));
builder.Services.AddScoped(typeof(ILinkStatsService), typeof(LinkStatService));
builder.Services.AddScoped(typeof(ICommunicationService), typeof(CommunicationService));
builder.Services.AddScoped(typeof(IMiniPageService), typeof(MiniPageService));

builder.Services.AddMemoryCache();
builder.Services.AddHttpClient<LinkPreviewService>();

builder.Services.AddRazorPages();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
    options.LogoutPath = "/logout";
    options.AccessDeniedPath = "/access-denied";
});

var app = builder.Build();

// --- UPDATED FOR RAILWAY PROXY ---
// Railway uses a proxy that handles HTTPS. These settings ensure your app 
// knows the original user's IP and protocol.
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
    // Clear known networks because Railway's proxy IP might change
    KnownNetworks = { },
    KnownProxies = { }
});

if (!app.Environment.IsDevelopment())
{
    app.UseStatusCodePagesWithReExecute("/error/{0}");
    app.UseExceptionHandler("/error");
    app.UseHsts();
}
else
{
    // Only use redirection in local development. 
    // Railway handles HTTPS at the domain level.
    app.UseHttpsRedirection();
}

app.UseRouting();
app.UseAuthentication(); // Ensure Authentication is called BEFORE Authorization
app.UseAuthorization();

app.UseStaticFiles(); // Added to serve images/CSS from wwwroot
app.MapStaticAssets();

app.MapRazorPages();

app.MapControllerRoute(
    name: "areas",
    pattern: "app/{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();