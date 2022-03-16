using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
             CreateHostBuilder(args).Build().Run();
           // BuildWebHost(args).Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>         
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(o => { o.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(10); });
                    webBuilder.UseStartup<Startup>();
                });

        //public static IWebHost BuildWebHost(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)           
        //        .UseStartup<Startup>().UseIISIntegration()
        //        .UseKestrel(o => { o.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(10); })
        //        .Build();
    }
}

