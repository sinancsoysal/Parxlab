using Parxlab.Data;
using Parxlab.Entities;
using Parxlab.Repository.Interfaces;
using System.Data;

namespace Parxlab.Repository.Repositories
{
    public class SensorRepository : Repository<Sensor>, ISensorRepository
    {
        public SensorRepository(ApplicationDbContext _context, IDbConnection _connection) : base(_context, _connection)
        {
        }
    }
}
