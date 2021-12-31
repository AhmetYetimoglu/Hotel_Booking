using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using business.Abstract;
using business.Concrete;
using data.Abstract;
using data.Concrete.EfCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using webui.Identity;

namespace webui
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Kullancı Database Ayarları
            services.AddDbContext<ApplicationContext>(optinos => optinos.UseSqlite("Data Source=shopDb"));
            services.AddIdentity<User,IdentityRole>().AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options=>{
                //password
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;

                // Lockout - Kullanıcın hesabının kilitlenip kilitlenmemesi durumu
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.AllowedForNewUsers = true;

                // options.User.AllowedUserNameCharacters = "";
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                
            });
            //cookei ayarları
            services.ConfigureApplicationCookie(options=>{
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";
                options.AccessDeniedPath = "/account/accessdenied";
                //cookie süresi varsayılan olarak 20 dk süre verir
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(365);
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                    Name = ".Proje.Security.Cookie"
                };
            });
            // Bir interface'i çağırabileceğiz ve interface çağrıldığında dolu olan Repository döndürülecektir.
            services.AddScoped<IProductRepository, EfCoreProductRepository>();
            services.AddScoped<IProductService, ProductManager>();
            // Controller kullanabileceğiz
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /// wwwroot dizinine ulaşır
            app.UseStaticFiles();

            // Farklı bir klasöre ulaşmak için
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(),"node_modules")), RequestPath = "/modules"
            });
            if (env.IsDevelopment())
            {
                //İlgili verilen değerler veritabanına eklenir.
                SeedDataBase.Seed();
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            // localhost:5000
            // localhost:5000/product/details/2
            // localhost:5000/product/list/3
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "adminproductlist",
                    pattern: "admin/products",
                    defaults: new {controller="Admin",action="ProductList"}
                );
                endpoints.MapControllerRoute(
                    name: "adminproductlist",
                    pattern: "admin/products/{id?}",
                    defaults: new {controller="Admin",action="Edit"}
                );
                // endpoints.MapControllerRoute(
                //     name: "productdetails",
                //     pattern: "{url}",
                //     defaults: new {controller="Shop",action="details"}
                // );
                //url products girilirse Shop controllerının list metotuna gönderilir
                
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern:"{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
