using System.Data;
using Parxlab.Data;
using Parxlab.Entities;
using Parxlab.Repository.Interfaces;

namespace Parxlab.Repository.Repositories
{
    public class ParkRepository:Repository<Park>,IParkRepository
    {
        public ParkRepository(ApplicationDbContext context, IDbConnection connection) : base(context, connection)
        {
        }
    }
}
