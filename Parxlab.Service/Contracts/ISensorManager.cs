
using System.Threading.Tasks;

namespace Parxlab.Service.Contracts
{
    public interface ISensorManager
    {
        void StartListener(string ip, int port);
    }
}
