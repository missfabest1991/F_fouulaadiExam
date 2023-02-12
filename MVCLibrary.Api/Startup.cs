using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVCLibrary.Data.Models;
using MVCLibrary.Infrastructure;
using MVCLibrary.Infrastructure.RepositoryClasses;
using MVCLibrary.Infrastructure.RepositoryInterfaces;
using MVCLibrary.Models;
using MVCLibrary.Services.ServiceClasses;
using MVCLibrary.Services.ServiceInterfaces;
using MVCLibrary.Utilites;
using System;

namespace MVCLibrary
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<MVCContext>(options => options.UseSqlServer(connectionString));

            ConfigurationHelper.InitConfig(configuration);

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddControllersWithViews();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ILibraryRepository, LibraryRepository>();
            services.AddScoped<ILibraryService, LibraryService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookDetailService, BookDetailService>();
            services.AddScoped<IBookDetailRepository, BookDetailRepository>();
            services.AddScoped<ICategoryService, CategoriService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProvinceRepository, ProvinceRepository>();
            services.AddScoped<IProvinceService, ProvinceServicee>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ICityService, CityService>();
            //services.AddIdentity<User, UserRole>(config =>
            //{
            //    config.User.RequireUniqueEmail = true;
            //});

            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
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

            app.UseEndpoints(endPoint =>
            {
                endPoint.MapControllers();
                endPoint.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
                endPoint.MapRazorPages();
            });
        }
    }
}