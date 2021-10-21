using System.Data;
using Parxlab.Data;
using Parxlab.Entities;
using Parxlab.Repository.Interfaces;

namespace Parxlab.Repository.Repositories
{
    public class ActivityLogRepository:Repository<ActivityLog>,IActivityLogRepository
    {
        public ActivityLogRepository(ApplicationDbContext context, IDbConnection connection) : base(context, connection)
        {
        }

    }
}
