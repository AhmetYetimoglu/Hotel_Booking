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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace webui
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
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

            app.UseRouting();

            // localhost:5000
            // localhost:5000/product/details/2
            // localhost:5000/product/list/3
            app.UseEndpoints(endpoints =>
            {
                //url products girilirse Shop controllerının list metotuna gönderilir
                endpoints.MapControllerRoute(
                    name: "products",
                    pattern: "products",
                    defaults: new {controller="Shop",action="list"}
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern:"{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
