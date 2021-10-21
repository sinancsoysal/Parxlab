using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Parxlab.IocConfig.Extensions;

namespace Parxlab.IocConfig.Service
{
    public static class AddServicesExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services,
            IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            services.AddMainServices(configuration);
            services.AddCustomIdentityServices(configuration, webHostEnvironment);
            return services;
        }
    }
}