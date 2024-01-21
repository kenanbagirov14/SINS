using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FontEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: true)
             .Build();

            var host = new WebHostBuilder()
                .UseKestrel(options =>
                {
                    options.Limits.MaxRequestHeadersTotalSize = 1024 * 1024;
                    options.Limits.MaxRequestLineSize = 1024 * 1024;
                    options.Limits.MaxRequestBodySize = null;
                    options.Listen(System.Net.IPAddress.Parse(config["IP"]), int.Parse(config["port"]), listenOptions =>
                    {
                        listenOptions.UseHttps(config["cert"], config["pass"]);
                    });
                })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseConfiguration(config)
                .UseStartup<Startup>()
                .Build();

            host.Run();

            //RecurringJob.AddOrUpdate(
            //    () => Console.WriteLine("Recurring!"),
            //    Cron.Minutely);
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
