using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Parxlab.Data;
using Parxlab.Data.Dtos.User;
using Parxlab.Service.Contracts.Identity;

namespace Parxlab.Service.Contracts.Impl.Identity
{
    public class IdentityDbInitializer : IIdentityDbInitializer
    {
        private readonly IIdentityService _identityService;
        private readonly ILogger<IdentityDbInitializer> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public IdentityDbInitializer(
            IIdentityService identityService,
            IServiceScopeFactory scopeFactory,
            ILogger<IdentityDbInitializer> logger
            )
        {
            _identityService = identityService;
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        /// <summary>
        /// Applies any pending migrations for the context to the database.
        /// Will create the database if it does not already exist.
        /// </summary>
        public void Initialize()
        {
            using var serviceScope = _scopeFactory.CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            //context?.Database.Migrate();
        }

        /// <summary>
        /// Adds some default values to the IdentityDb
        /// </summary>
        public void SeedData()
        {
            using var serviceScope = _scopeFactory.CreateScope();
            var identityDbSeedData = serviceScope.ServiceProvider.GetService<IIdentityDbInitializer>();
            identityDbSeedData?.SeedDatabaseWithAdminUserAsync();
        }

        public async Task SeedDatabaseWithAdminUserAsync()
        {
            //Create the `Admin` Role if it does not exist
            var adminRole =await _identityService.CreateRole(Constants.AdminRole);
            if(adminRole.IsSuccess)
                _logger.LogInformation("Yönetici Rolü Oluşturuldu");
            else
                _logger.LogWarning($"Yönetici Rolü Oluşturulamadı. {adminRole.Errors.FirstOrDefault()}");

            //Create the `Manager` Role if it does not exist
            var managerRole = await _identityService.CreateRole(Constants.ManagerRole);
            if (managerRole.IsSuccess)
                _logger.LogInformation("Yönetici Rolü Oluşturuldu");
            else
                _logger.LogWarning($"Yönetici Rolü Oluşturulamadı. {managerRole.Errors.FirstOrDefault()}");

            //Create the `Client` Role if it does not exist
            var clientRole = await _identityService.CreateRole(Constants.ClientRole);
            if (clientRole.IsSuccess)
                _logger.LogInformation("Müşteri Rolü Oluşturuldu");
            else
                _logger.LogWarning($"İstemci Rolü Oluşturulamadı. {clientRole.Errors.FirstOrDefault()}");

            //Create the `Staff` Role if it does not exist
            var staffRole = await _identityService.CreateRole(Constants.StaffRole);
            if (staffRole.IsSuccess)
                _logger.LogInformation("Oluşturulan Personel Rolü");
            else
                _logger.LogWarning($"Staff Role Creation failed. {staffRole.Errors.FirstOrDefault()}");

            var adminUserResult = await _identityService.Register(new RegisterUserDto()
            {
                Username = "admin",
                Password = "sevgili_1",
                FirstName = "Amin",
                LastName = "Parsa",
                Email = "aminparsa18@gmail.com",
                PhoneNumber = "+905316335119",
                Role = Constants.AdminRole
            });

            if (!adminUserResult.IsSuccess)
                _logger.LogWarning($"Yönetici Kullanıcı CreateAsync başarısız oldu. {adminUserResult.Errors.FirstOrDefault()}");
        }
    }
}