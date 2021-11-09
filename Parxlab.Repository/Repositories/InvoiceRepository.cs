using System.Data;
using Parxlab.Data;
using Parxlab.Entities;
using Parxlab.Repository.Interfaces;

namespace Parxlab.Repository.Repositories
{
    public class InvoiceRepository:Repository<Invoice>,IInvoiceRepository
    {
        public InvoiceRepository(ApplicationDbContext context, IDbConnection connection) : base(context, connection)
        {
        }
    }
}
