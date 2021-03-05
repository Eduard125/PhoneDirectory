using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhoneDirectory.BLL.Interfaces;
using PhoneDirectory.BLL.Services;
using PhoneDirectory.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneDirectory.WEB
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
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => //CookieAuthenticationOptions
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Home/Login");
                });

            // Регистрация сервисов BLL через внедрение зависимостей
            services.AddSingleton(a => new EFUnitOfWork(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSingleton<IHomeService, HomeService>();
            services.AddSingleton<IAdminService, AdminService>();
            services.AddSingleton<IEmployeeService, EmployeeService>();



            services.AddControllersWithViews();

            services.AddOptions();
            services.Configure<Models.MyAppConfig>(Configuration.GetSection("MyAppConfig"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, EFUnitOfWork unitOfWork, IHomeService homeService, IAdminService adminService, IEmployeeService employeeService)
        {
            // Инициализация сервисов BLL
            homeService.InitDB(unitOfWork);
            adminService.InitDB(unitOfWork);
            employeeService.InitDB(unitOfWork);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }
           // app.UseHttpsRedirection();
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
