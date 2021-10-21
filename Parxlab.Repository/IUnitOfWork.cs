using System.Threading.Tasks;
using Parxlab.Repository.Interfaces;

namespace Parxlab.Repository
{
    public interface IUnitOfWork
    {
        IActivityLogRepository ActivityLog { get; }
        IInvoiceRepository Invoice { get;  }
        IParkRepository Park { get; }
        IParkUsageRepository ParkUsage { get; }
        IRefreshTokenRepository RefreshToken{ get; }
        IReservedRepository Reserved { get;}
        ISensorRepository Sensor { get;  }

        Task<int> Commit();
    }
}
