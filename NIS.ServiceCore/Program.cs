using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace NIS.ServiceCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: false)
             .Build();

            var host = new WebHostBuilder()
                 //TODO uncomment all line befor publish
                 .UseKestrel(options =>
                 {
                     options.Limits.MaxRequestHeadersTotalSize = 1024 * 1024;
                     options.Limits.MaxRequestLineSize = 1024 * 1024;
                     options.Limits.MaxRequestBodySize = null;
                     options.Listen(IPAddress.Loopback, 700);
                     options.Listen(System.Net.IPAddress.Parse(config["IP"]), int.Parse(config["port"]), listenOptions =>
                     {
                         if (bool.Parse(config["ssl"]))
                         {
                             listenOptions.UseHttps(config["cert"], config["pass"]);
                         }
                     });
                 })
                 .UseKestrel()
                 .UseIISIntegration()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseConfiguration(config)
                .UseStartup<Startup>()

                .Build();

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
