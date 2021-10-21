using System.Collections.Generic;
using Parxlab.Data;
using Parxlab.Entities;
using Parxlab.Repository.Interfaces;
using System.Data;
using System.Threading.Tasks;
using Parxlab.Data.Dtos.Sensor;
using RepoDb;

namespace Parxlab.Repository.Repositories
{
    public class SensorRepository : Repository<Sensor>, ISensorRepository
    {
        public SensorRepository(ApplicationDbContext _context, IDbConnection _connection) : base(_context, _connection)
        {
        }

        public Task<IEnumerable<SensorDto>> GetAllDto()
        {
            return connection.ExecuteQueryAsync<SensorDto>(@"SELECT Ip,Port,Status,WPSDId FROM [Sensor]");
        }

        public Task<int> StartListening(string sensorId)
        {
            return connection.ExecuteNonQueryAsync("UPDATE [Sensor] SET [Status] = 1 WHERE [WPSDId] = @sensorId",
                new { sensorId });
        }

        public Task<int> StopListening(string sensorId)
        {
            return connection.ExecuteNonQueryAsync("UPDATE [Sensor] SET [Status] = 0 WHERE [WPSDId] = @sensorId",
                new { sensorId });
        }
    }
}
