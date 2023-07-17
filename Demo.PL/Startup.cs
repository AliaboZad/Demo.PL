using Demo.BLL;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositories;
using Demo.DAL.Context;
using Demo.DAL.Models;
using Demo.PL.MappingProfile;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL
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
            services.AddControllersWithViews();

            #region Connection With Database
            services.AddDbContext<MVCDbContext>(option =>
                {
                    option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                });
            // To Create Object from DbContext 
            #endregion
            //services.AddSingleton<IDepartmentRepositry , DepartmentRepositry>();
            //services.AddTransient<IEmployeeRepositry , EmployeeRepositries>();

            ///Befor Unit Of Work

            //services.AddScoped<IDepartmentRepositry, DepartmentRepositry>();
            //services.AddScoped<IEmployeeRepositry, EmployeeRepositries>();

            ///After Unit Of Work
            services.AddScoped<IUniteOfWork , UnitOfWork>();

            //دي بعملها عشان اقدر اعدل والبرنامج شغال واعمل ريفرش بس وهو هيشتغل بعد التعديل
            services.AddRazorPages().AddRazorRuntimeCompilation();

            services.AddAutoMapper(M => M.AddProfile(new EmployeeProfile()));
            services.AddAutoMapper(D => D.AddProfile(new DepartmentProfile()));
            

            //services.AddScoped<UserManager<ApplicationUser>>();
            
            services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                option.Password.RequireNonAlphanumeric= true;
                option.Password.RequireUppercase= true;
                option.Password.RequireDigit= true;
            })
                .AddEntityFrameworkStores<MVCDbContext>()
                .AddDefaultTokenProviders();

			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(option =>
                {
                    option.LoginPath = "Account/Login";
                    option.AccessDeniedPath = "Home/Error"; 
                });
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
