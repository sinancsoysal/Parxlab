using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Parxlab.Common.Api;
using Parxlab.Data;
using Parxlab.Entities.Identity;
using Parxlab.Service.Contracts.Identity;
using Parxlab.Service.Contracts.Impl.Identity;

namespace Parxlab.IocConfig.Extensions
{
    public static class AddIdentityWithOptionsExtensions
    {
        public static IServiceCollection AddIdentityWithOptions(this IServiceCollection services,
            IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            services.AddIdentity<User, Role>(
                    options =>
                    {
                        //Configure Password
                        options.Password.RequireDigit = true;
                        options.Password.RequiredLength = 6;
                        options.Password.RequiredUniqueChars = 1;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequireUppercase = false;
                        options.User.AllowedUserNameCharacters =
                            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._+";
                        options.User.RequireUniqueEmail = false;
                        options.SignIn.RequireConfirmedEmail = false;
                        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20);
                        options.Lockout.MaxFailedAccessAttempts = 3;
                    })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddErrorDescriber<ApplicationIdentityErrorDescriber>()
                .AddDefaultTokenProviders();
            services.AddTransient<ITokenService, TokenService>();
            // configure strongly typed settings objects
            var appSettingsSection = configuration.GetSection("Jwt");
            services.Configure<Jwt>(appSettingsSection);
            // configure jwt authentication
            var appSettings = appSettingsSection.Get<Jwt>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };

            services.AddSingleton(tokenValidationParameters);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = tokenValidationParameters;
                x.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    },
                    //OnMessageReceived = context =>
                    //{
                    //    var accessToken = context.Request.Query["access_token"];

                    //    // If the request is for our hub...
                    //    var path = context.HttpContext.Request.Path;
                    //    if (!string.IsNullOrEmpty(accessToken) &&
                    //    (path.StartsWithSegments("/gamehub")))
                    //    {
                    //        // Read the token out of the query string
                    //        context.Token = accessToken;
                    //    }
                    //    return Task.CompletedTask;
                    //}
                };
            });
            services.AddAuthorization(option =>
            {
                option.AddPolicy("Company", policy => policy.RequireClaim("Company"));
                option.AddPolicy("Invoice", policy => policy.RequireClaim("Invoice"));
                option.AddPolicy("MainExpense", policy => policy.RequireClaim("MainExpense"));
                option.AddPolicy("Query", policy => policy.RequireClaim("Query"));
                option.AddPolicy("Report", policy => policy.RequireClaim("Report"));
                option.AddPolicy("Excel", policy => policy.RequireClaim("Excel"));
                option.AddPolicy("Settings", policy => policy.RequireClaim("Settings"));
            });

            var keysFolder = Path.Combine(webHostEnvironment.ContentRootPath, "Keys");
            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(keysFolder))
                .SetApplicationName("DbhSystem");
            return services;
        }
    }
}