using crudProdutos.Data;
using crudProdutos.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace crudProdutos
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
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true; //default = false
                options.Password.RequireNonAlphanumeric = false; //default = true
                options.Password.RequireUppercase = false; //default = true
                options.Password.RequireLowercase = false; //default = true
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3); //default = 3
                options.Lockout.MaxFailedAccessAttempts = 3; //default = 5
                options.SignIn.RequireConfirmedAccount = false; //default = false
                options.SignIn.RequireConfirmedEmail = false; //default = false
                options.SignIn.RequireConfirmedPhoneNumber = false; //default =false
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                options.LoginPath = "/Login";
                options.AccessDeniedPath = "/Login";
                options.SlidingExpiration = true;
            });

            services.AddAuthorization();

            services.AddRazorPages();

            //Injeção de Dependência 
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlite(Configuration.GetConnectionString("ApplicationDbContext")));
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
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });

            var defaultCulture = new CultureInfo("pt-BR"); //Constante para pt-BR
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultCulture),
                SupportedCultures = new List<CultureInfo> { defaultCulture },
                SupportedUICultures = new List<CultureInfo> { defaultCulture }
            };
            app.UseRequestLocalization(localizationOptions);
        }
    }
}
