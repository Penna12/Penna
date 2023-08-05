using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using Penna.Data.Seeds;
using FluentValidation.AspNetCore;
using Penna.Data.EntityFramework.Contexts;
using Penna.Entities.Models;
using Penna.Business.DependencyResolver;
using Penna.Business.Helper;
using Penna.Web.ClaimRequirements;
using Microsoft.AspNetCore.Authorization;
using Penna.Business.Filters;
using Penna.Core.Utilities.Constants;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

namespace Penna.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddHttpContextAccessor();
            services.AddSession();
            
            services.AddCustomDependencies(Configuration); // DbContext, Repository, IUserClaimsPrincipalFactory
            // Identity
            services.AddDefaultIdentity<AppUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();
                //.AddClaimsPrincipalFactory<MyUserClaimsPrincipalFactory>();

            // Identity settings
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            //Cookie Configure
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.Cookie.Name = "PennaYapiDekorasyon";
                options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
                options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest;
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(20);
                options.SlidingExpiration = true;
            });

            services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, AppUserClaimsPrincipalFactory>();

            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation()
                .AddFluentValidation()
                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);  // Validation Filter Service
            services.AddMvc()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();

            services.AddLocalization(opt => { opt.ResourcesPath = "Resources"; });
            services.Configure<RequestLocalizationOptions>(opt => {
                var supportedCulture = new List<CultureInfo>() {
                    new CultureInfo("tr-TR"),
                    new CultureInfo("en-US"),
                    new CultureInfo("ar-SA")
                };
                opt.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("tr-TR");
                opt.SupportedCultures = supportedCulture;
                opt.SupportedUICultures = supportedCulture;

                opt.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new QueryStringRequestCultureProvider(),
                    new CookieRequestCultureProvider(),
                    new AcceptLanguageHeaderRequestCultureProvider()
                };
            });

            //services.AddRazorPages();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ProjectSelected", policy =>
                    policy.Requirements.Add(new ProjectSelectedRequirement()));
            });

            services.AddSingleton<IAuthorizationHandler, ProjectSelectedHandler>();
            services.AddScoped<ProjectSelectedCheckFilter>();
            services.AddScoped<BlockSelectedCheckFilter>();
            services.AddScoped<ProjectUnselectedFilter>();

            services.Configure<Entities.DTOs.SMTPConfigModel>(Configuration.GetSection("SMTPConfig"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AppDbContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Global Hata Yakalama
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            SD.RootPath = env.ContentRootPath;

            Utility.Service.App.Services = app.ApplicationServices;
            Entities.DTOs.Toolbar.Init();

            app.UseHttpsRedirection();
            app.UseSession();
            app.UseStaticFiles();

            SeedData.Seed(db, userManager, roleManager).Wait();

            app.UseRouting();
            // Identity
            app.UseAuthentication();
            app.UseAuthorization();

            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "Explorer",
                    pattern: "Explorer/{action=Index}/{*path}",
                    defaults: new { controller = "Explorer" });
                // endpoints.MapRazorPages();
            });
        }
    }
}
