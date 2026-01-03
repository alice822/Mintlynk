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

// Add database context
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("AppConnection")));


// Add Identity services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    //Signin settings.
    options.SignIn.RequireConfirmedAccount = true;
    options.SignIn.RequireConfirmedEmail = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register the application services
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

// Configure the login path
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
    options.LogoutPath = "/logout";
    options.AccessDeniedPath = "/access-denied";
});

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseStatusCodePagesWithReExecute("/error/{0}");
    app.UseExceptionHandler("/error");
    
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

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