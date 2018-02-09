using CommonModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NetworkUtils;
using static System.Diagnostics.Trace;

namespace PassengerAPI
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

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    //options.Authority = "http://localhost:5000";
                    options.Authority = "http://172.30.225.93";
                    options.RequireHttpsMetadata = false;
                    
                    options.ApiName = "api1";
                });
            //load config from appsettings.json
            services.Configure<AppSettings>(_configuration.GetSection("AppSettings"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IOptions<AppSettings> appSettingsSection)
        {
            app.UseAuthentication();

            app.UseMvc();

            //print in output
            HostHelper.OutputHostnameInfo(appSettingsSection.Value.ApplicationTitle);

        }
    }
}
