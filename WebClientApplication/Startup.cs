using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Data.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Catalog.Orders;
using Services.Catalog.ProductCategories;
using Services.Catalog.Products;
using Services.Catalog.Users;
using Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClientApplication
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
            services.AddRazorPages();
            services.AddDbContext<ShopDbContext>(options =>
                        options.UseSqlServer(Data.SystemConstants.SQLcnn));

            services.AddHttpClient();
            services.AddNotyf(config => { config.DurationInSeconds = 3; config.IsDismissable = true; config.Position = NotyfPosition.TopRight; });

            // Add services to the container.
            services.AddRazorPages();
            services.AddMvc();

            services.AddDistributedMemoryCache();
            services.AddSession(option =>
            {
                option.Cookie.Name = "CartSession";
                option.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            services.AddControllers();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IProductCategoryService, ProductCategoryService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IStorageService, FileStorageService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IOrderService, OrderService>();

            IMvcBuilder build = services.AddRazorPages();
            build.AddRazorRuntimeCompilation();
            build.Services.AddMemoryCache();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseNotyf();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "chi-tiet-san-pham", pattern: "product-{id}.html", new { controller = "Product", action = "ProductDetail" });

                endpoints.MapControllerRoute(
                name: "them-vao-gio-hang", pattern: "AddToCart-{id}.html", new { controller = "Product", action = "AddToCart" });

                endpoints.MapControllerRoute(
                name: "danh-muc-san-pham", pattern: "category-{id}.html", new { controller = "Category", action = "ProductInCategory" });

                endpoints.MapControllerRoute(
                name: "dang-nhap-nguoi-dung", pattern: "loginuser.html", new { controller = "Login", action = "Index" });

                endpoints.MapControllerRoute(
                name: "Home", pattern: "Index.html", new { controller = "Home", action = "Index" });


                endpoints.MapControllerRoute(name: "default",
               pattern: "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
