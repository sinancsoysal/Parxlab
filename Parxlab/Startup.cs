using System.Data;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Parxlab.Data;
using Parxlab.IocConfig.Middleware;
using Parxlab.IocConfig.Service;
using RepoDb;

namespace Parxlab
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("ParxlabHangfireContext")));
            services.AddHangfireServer();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ParxlabContext")));
            SqlServerBootstrap.Initialize();
            services.AddTransient<IDbConnection>(_ =>
                new SqlConnection(Configuration.GetConnectionString("ParxlabContext")));
            services.AddCustomServices(Configuration, WebHostEnvironment);
            services.AddHostedService<SensorWorker>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.AddCustomMiddleware();
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[]
                {
                    new HangfireAuthorizationFilter()
                }
            });
            app.Run(async context => await Task.Run(() => context.Response.Redirect("/swagger")));
        }
    }
}