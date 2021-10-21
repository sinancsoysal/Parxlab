using System;
using System.Threading.Tasks;
using Parxlab.Repository;

namespace Parxlab.Service.Contracts
{
    public interface IFireForgetRepositoryHandler
    {
        void Execute(Func<IUnitOfWork, Task> databaseWork);
    }
}
