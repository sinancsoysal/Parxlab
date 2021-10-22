using System.Linq;
using System.Net;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.Generation.Processors.Security;
using Parxlab.Common.Api;
using Parxlab.Common.Extensions;
using Parxlab.Data;
using Parxlab.IocConfig.Formatters;
using Parxlab.IocConfig.Hubs;
using Parxlab.Repository;
using Parxlab.Service.Contracts;
using Parxlab.Service.Models;
using Serilog;
using Utf8Json.Resolvers;

namespace Parxlab.IocConfig.Extensions
{
    public static class MainExtentions
    {
        public static IServiceCollection AddMainServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAntiforgery();
            services.AddCors(options =>
            {
                options.AddPolicy("myPolicy",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:8080", "https://localhost:8080", "http://localhost",
                                "https://localhost", "https://dbh-systems-web-app.vercel.app").AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
            services.AddControllers(option =>
                {
                    option.OutputFormatters.Clear();
                    option.OutputFormatters.Add(new Utf8JsonOutputFormatter(StandardResolver.Default));
                    option.InputFormatters.Clear();
                    option.InputFormatters.Add(new Utf8JsonInputFormatter());
                }).AddJsonOptions(opt => opt.JsonSerializerOptions.PropertyNamingPolicy = null);
              //  .AddFluentValidation(fv => { fv.RegisterValidatorsFromAssemblyContaining<CreateCompanyValidator>(); });
            services.AddApiVersioning(setup =>
            {
                setup.DefaultApiVersion = new ApiVersion(1, 0);
                setup.AssumeDefaultVersionWhenUnspecified = true;
                setup.ReportApiVersions = true;
            });
            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("DbhSystemContext"))
                .AddProcessAllocatedMemoryHealthCheck(200, tags: new[] {"memory"});
            services.AddHealthChecksUI().AddInMemoryStorage();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            services.Scan(scan =>
            {
                scan.FromAssemblyOf<IUnitOfWork>()
                    .AddClasses(c => c.AssignableTo<IUnitOfWork>())
                    .AsImplementedInterfaces().WithScopedLifetime()
                    .FromAssemblyOf<IEmailSender>().AddClasses()
                    .AsImplementedInterfaces().WithScopedLifetime();
            });
            services.AddDistributedMemoryCache();
            services.AddOptions<NotificationHubOptions>()
                .Configure(configuration.GetSection("NotificationHub").Bind)
                .ValidateDataAnnotations();
            services.AddAutoMapper(cfg => cfg.AddProfile<AutoMapping>());
            services.AddSwaggerDocument(setting =>
            {
                setting.DocumentName = "Doc name";
                
                setting.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Parxlab API";
                    document.Info.Description = "Parking Automation System";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new OpenApiContact
                    {
                        Name = "Amin Parsa",
                        Email = "aminparsa18@gmail.com",
                        Url = "https://aminparsa.me"
                    };
                    document.Info.License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = "https://opensource.org/licenses/MIT"
                    };
                };
                setting.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: {your JWT token}."
                });

                setting.OperationProcessors.Add(
                    new AspNetCoreOperationSecurityScopeProcessor("JWT"));
                setting.GenerateEnumMappingDescription = true;
            });
            services.AddSignalR();
            return services;
        }

        public static void UseMainMiddlewares(this IApplicationBuilder app)
        {
            app.UseHsts();
            app.UseStatusCodePages(async context =>
            {
                if (context.HttpContext.Response.StatusCode == 401)
                {
                    await context.HttpContext.Response.WriteAsync(new ApiResult()
                    {
                        Errors = new[] {"token doğrulanmadı"},
                        StatusCode = ApiResultStatusCode.UnAuthorized
                    }.ToString()!);
                }
                else
                {
                    await context.HttpContext.Response.WriteAsync(new ApiResult()
                    {
                        Errors = new[] {"İç hata"},
                        StatusCode = ApiResultStatusCode.ServerError
                    }.ToString()!);
                }
            });
            app.UseHealthChecksPrometheusExporter("/HealthCheck");
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        await context.Response.WriteAsync(contextFeature.Error.DetailedMessage());
                    }
                });
            });
            
            app.UseOpenApi(); // serve documents (same as app.UseSwagger())
            app.UseSwaggerUi3(); // serve Swagger UI

            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();
            app.UseRouting();
            app.UseCors("myPolicy");

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chathub");
                endpoints.MapHealthChecks("/hc");
                endpoints.MapHealthChecksUI();
            });
            app.CallDbInitializer();
        }
    }
}