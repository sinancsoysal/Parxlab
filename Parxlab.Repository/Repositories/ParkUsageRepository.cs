using System.Data;
using System.Threading.Tasks;
using Parxlab.Data;
using Parxlab.Entities;
using Parxlab.Repository.Interfaces;
using RepoDb;

namespace Parxlab.Repository.Repositories
{
    public class ParkUsageRepository:Repository<ParkUsage>,IParkUsageRepository
    {
        public ParkUsageRepository(ApplicationDbContext context, IDbConnection connection) : base(context, connection)
        {
        }

        public Task<int> StopUsage(string sensorId)
        {
            return connection.ExecuteNonQueryAsync("UPDATE [Sensor] SET [Status] = 1 WHERE [UserId] = @userId",
                new { sensorId });
        }
    }
}
