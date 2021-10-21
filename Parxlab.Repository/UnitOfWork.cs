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
        private IActivityLogRepository activityLog;
        private IInvoiceRepository invoice;
        private IParkRepository park;
        private IParkUsageRepository parkUsage;
        private IRefreshTokenRepository refreshToken;
        private IReservedRepository reserved;
        private ISensorRepository sensor;

        public UnitOfWork(ApplicationDbContext context, IDbConnection connection)
        {
            this._context = context;
            this._connection = connection;
        }

        public IActivityLogRepository ActivityLog => activityLog ??= new ActivityLogRepository(_context, _connection);
        public IInvoiceRepository Invoice => invoice ??= new InvoiceRepository(_context, _connection);
        public IParkRepository Park => park ??= new ParkRepository(_context, _connection);
        public IParkUsageRepository ParkUsage => parkUsage ??= new ParkUsageRepository(_context, _connection);

        public IRefreshTokenRepository RefreshToken =>
            refreshToken ??= new RefreshTokenRepository(_context, _connection);
        public IReservedRepository Reserved => reserved ??= new ReservedRepository(_context, _connection);
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
