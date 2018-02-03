using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog.Extensions.Logging;
using NLog.Web;
using RestTestWebApp.Controllers;
using RestTestWebApp.Extensions;
using RestTestWebApp.Services;

namespace RestTestWebApp
{
    public class Startup
    {
        public IConfiguration _configuration { get; }
        public IHostingEnvironment _hostingEnvironment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        

        //// This method gets called by the runtime. Use this method to add services to the container.
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddMvc();

        //    services.AddLocalization(options => options.ResourcesPath = "Resources");

        //    //make these services available for the application
        //    services.AddSingleton<IUserService, UserService>();

        //    services.Configure<EmailServiceOptions>(_configuration.GetSection("Email"));
        //    services.AddSingleton<IEmailService, EmailService>();
            
        //}

        public void ConfigureCommonServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            
            //configure services with appsettings.json
            services.Configure<AppSettings>(_configuration.GetSection("AppSettings"));
            services.Configure<EmailServiceSettings>(_configuration.GetSection("Email"));

            //make these services available for the application
            services.AddSingleton<IUserService, UserService>();
            services.AddEmailService(_hostingEnvironment, _configuration);
            services.AddRouting();
            services.AddSession(o =>
            {
                o.IdleTimeout = TimeSpan.FromMinutes(30);
            });
        }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            System.Diagnostics.Trace.WriteLine("Using Development Environment");
            ConfigureCommonServices(services);
        }

        public void ConfigureStagingServices(IServiceCollection services)
        {
            System.Diagnostics.Trace.WriteLine("Using Staging Environment");
            ConfigureCommonServices(services);
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            System.Diagnostics.Trace.WriteLine("Using Production Environment");
            ConfigureCommonServices(services);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IOptions<AppSettings> appSettingsSection)
        {
            AppSettings appSettings = appSettingsSection.Value;
            System.Diagnostics.Trace.WriteLine($"This appsetting was Injected by netcore: {appSettings.ApplicationTitle}");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCommunicationMiddleware();

            var supportedCultures = new[] {
                new CultureInfo("en-US"),
                new CultureInfo("en"),
                new CultureInfo("es"),
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(new CultureInfo("es")),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
                RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new QueryStringRequestCultureProvider
                    {
                        QueryStringKey = "culture",
                        UIQueryStringKey = "ui-culture"
                    }
                }
            });

            app.UseMvc();

            env.ConfigureNLog("nlog.config");
            // make sure other languages chars wont mess up
            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            //add NLog to asp.net core
            loggerFactory.AddNLog();

            //add Nlog.Web
            app.AddNLogWeb();
            

            
        }
    }
}
