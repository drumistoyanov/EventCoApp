using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EventCoApp.DataAccessLibrary.DataAccess;
using EventCoApp.DataAccessLibrary.Models;
using EventCoApp.Services.Admin.Interfaces;
using EventCoApp.Services.Admin;
using AutoMapper;
using System.Threading.Tasks;
using System;
using EventCoApp.Common;
using EventCoApp.Services.Messaging;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.IO;
using EventCoApp.Services.Middlewares;
using Microsoft.AspNetCore.Razor.TagHelpers;
using EventCoApp.Common.Helpers;

namespace EventCoApp.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }
        public IWebHostEnvironment Env { get; set; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<EventCoContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);
            //services.AddIdentity<User, Role>()
            //    .AddDefaultUI()
            //    .AddRoleManager<RoleManager<Role>>()
            //    .AddDefaultTokenProviders()
            //    .AddEntityFrameworkStores<EventCoContext>();

            services.AddIdentity<User, Role>(options =>
            {
                options.User.AllowedUserNameCharacters = null;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            })
               .AddEntityFrameworkStores<EventCoContext>()
                .AddDefaultUI()
               .AddUserStore<UserStore<User, Role, EventCoContext, int>>()
               .AddRoleStore<RoleStore<Role, EventCoContext, int>>()
               .AddRoleManager<RoleManager<Role>>()
               .AddDefaultTokenProviders();
            RegisterServiceLayer(services);
            services.AddCloudscribePagination();

            services.AddSession(opt =>
            {
                opt.Cookie.IsEssential = true;
            });
            services.AddAutoMapper(System.Reflection.Assembly.GetExecutingAssembly());
            services.AddControllersWithViews();
            IMvcBuilder builder = services.AddRazorPages();
#if DEBUG
            if (Env.IsDevelopment())
            {
                builder.AddRazorRuntimeCompilation();
            }
#endif
            services.AddAuthorization(options =>
            {
                options.AddPolicy("HasPermission", policy => policy.Requirements.Add(new Helpers.AuthorizationPolicies.HasPermissionRequirement()));
            });

            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            });

            //SendGrid
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);

            //Images
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
            services.AddMvc();

            //google analytics
            services.Configure<GoogleAnalyticsOptions>(options => Configuration.GetSection("GoogleAnalytics").Bind(options));

            // Register the TagHelperComponent
            services.AddTransient<ITagHelperComponent, GoogleAnalyticsTagHelperComponent>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            //UncommentForCreatingRoles
            //roleManager.EnsureRolesCreated().Wait();

            app.UseMiddleware(typeof(VisitorCounterMiddleware));

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                     name: "area",
                     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            // CreateRoles(serviceProvider).Wait();
        }
        private static void RegisterServiceLayer(IServiceCollection services)
        {
            services.AddScoped<IAdminUsersService, AdminUsersService>();
        }
    }
}
