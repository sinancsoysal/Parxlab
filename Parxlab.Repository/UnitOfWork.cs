using Parxlab.Data;
using Parxlab.Repository.Interfaces;
using Parxlab.Repository.Repositories;
using System.Data;
using System.Threading.Tasks;

namespace Parxlab.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IDbConnection _connection;
        private ISensorRepository sensor;

        public UnitOfWork(ApplicationDbContext context, IDbConnection connection)
        {
            this._context = context;
            this._connection = connection;
        }

        public ISensorRepository Sensor => sensor ??= new SensorRepository(_context, _connection);

        public Task<int> Commit()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.DisposeAsync();
        }
    }
}
