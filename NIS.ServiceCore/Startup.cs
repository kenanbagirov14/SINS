using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using HybridModelBinding;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NIS.BLCore.Hubs;
using NIS.DALCore.UnitOfWork;

namespace NIS.ServiceCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IConfigurationRoot ConfigurationRoot { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHsts(config =>
            {
                config.MaxAge = TimeSpan.FromDays(100);
                config.IncludeSubDomains = true;
                config.Preload = true;
            });
            // Add framework services.
            //services.AddDbContext<NISContext>(options =>options.UseSqlServer(Configuration.GetSection("DefaultConnection").GetSection("connectionString").Value));
            services.AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();

            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddSingleton(Configuration);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = false,
                       ValidateAudience = false,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = "yourdomain.com",
                       ValidAudience = "yourdomain.com",
                       IssuerSigningKey = new SymmetricSecurityKey(
                           Encoding.UTF8.GetBytes("secretmecretsecret"))
                   };
                   // We have to hook the OnMessageReceived event in order to
                   // allow the JWT authentication handler to read the access
                   // token from the query string when a WebSocket or 
                   // Server-Sent Events request comes in.
                   options.Events = new JwtBearerEvents
                   {
                       OnMessageReceived = context =>
                       {
                           var accessToken = context.Request.Query["access_token"];

                           // If the request is for our hub...
                           var path = context.HttpContext.Request.Path;
                           if (!string.IsNullOrEmpty(accessToken) &&
                               (path.StartsWithSegments("/nisHub")))
                           {
                               // Read the token out of the query string
                               context.Token = accessToken;
                           }
                           return Task.CompletedTask;
                       }
                   };
               });
            services.AddResponseCompression();
            services.AddCors();
            // Add framework services.
            services.AddMvc(x =>
            {
                x.Conventions.Add(new HybridModelBinderApplicationModelConvention());
            });
            services.Configure<MvcOptions>(x =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var readerFactory = serviceProvider.GetRequiredService<IHttpRequestStreamReaderFactory>();

                x.ModelBinderProviders.Insert(0, new DefaultHybridModelBinderProvider(x.InputFormatters, readerFactory));
            });
            services.AddSignalR();

            
           
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider svp)
        {

            //ConfigureOAuth(app);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHsts();
            app.UseResponseCompression();
            app.UseAuthentication();
            app.UseStaticFiles();
            loggerFactory.AddDebug(LogLevel.Trace);
            loggerFactory.AddFile("Logs/core-{Date}.txt", isJson: true);

            System.Web.HttpContext.ServiceProvider = svp;
            System.Web.Hosting.HostingEnvironment.m_IsHosted = true;
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            app.UseSignalR(routes =>
            {
                routes.MapHub<NisHub>("/NisHub");
            });
            //app.Use(next => (context) =>
            //{
            //    var hubContext = (IHubContext<NisHub>)context
            //                        .RequestServices
            //                        .GetServices<IHubContext<NisHub>>();
            //    //...
            //});
            app.UseMvc();

            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"file")),
                RequestPath = new PathString("/file")
            });

            DALCore.Model.DBConnection.Connection = Configuration.GetSection("DBConnection").Value;
        }
    }
}
