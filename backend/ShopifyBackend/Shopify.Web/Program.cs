using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Shopify.Core.Data;
using Shopify.Core.Domain.Repositories;
using Shopify.Core.Domain.Services;
using Shopify.Core.Repositories;
using Shopify.Core.Services;
using NLog;
using NLog.Web;
using Shopify.Core.Utilities;

namespace Shopify.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Info("Init main");
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                builder.Host.UseNLog();

                //Database Configuration
                //builder.Services.AddDbContext<AppDbContext>(options =>
                //{
                //    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
                //});

                builder.Services.AddDbContext<ShopifyContext>(options =>
                {
                    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
                });

                builder.Services.AddScoped<Microsoft.Extensions.Logging.ILogger, Microsoft.Extensions.Logging.Logger<UnitOfWork>>();
                builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
                builder.Services.AddScoped<IUserService, UserService>();
                builder.Services.AddScoped<IProductService, ProductService>();
                builder.Services.AddScoped<IBrandService, BrandService>();
                builder.Services.AddScoped<ICategoryService, CategoryService>();

                builder.Services.AddSingleton<AzureBlobService>();


                // Add services to the container.
                builder.Services.AddControllersWithViews();

                //Authentication
                builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
                {
                    options.LoginPath = "/auth/login";
                    options.LogoutPath = "/auth/logout";
                    options.AccessDeniedPath = "/auth/accessdenied";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                    options.SlidingExpiration = true;
                });

                //Session
                builder.Services.AddSession(options =>
                {
                    options.IdleTimeout = TimeSpan.FromMinutes(20);
                    options.Cookie.HttpOnly = true;
                    options.Cookie.IsEssential = true;
                });

                var app = builder.Build();

                logger.Info("Application build successful");

                // Configure the HTTP request pipeline.
                if (!app.Environment.IsDevelopment())
                {
                    app.UseExceptionHandler("/Home/Error");
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }

                app.UseHttpsRedirection();
                app.UseStaticFiles();

                app.UseRouting();

                app.UseSession();
                app.UseAuthentication();

                app.UseAuthorization();


                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                app.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, ex.Message);
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }
    }
}
