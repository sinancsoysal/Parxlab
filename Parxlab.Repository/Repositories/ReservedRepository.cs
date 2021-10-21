using System.Data;
using Parxlab.Data;
using Parxlab.Entities;
using Parxlab.Repository.Interfaces;

namespace Parxlab.Repository.Repositories
{
    public class ReservedRepository:Repository<Reserved>,IReservedRepository
    {
        public ReservedRepository(ApplicationDbContext context, IDbConnection connection) : base(context, connection)
        {
        }
    }
}
