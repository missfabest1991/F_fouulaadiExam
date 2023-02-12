using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
        }
    }
}