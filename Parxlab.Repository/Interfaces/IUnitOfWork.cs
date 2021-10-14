using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parxlab.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        ISensorRepository Sensor { get; }

        Task<int> Commit();
    }
}
