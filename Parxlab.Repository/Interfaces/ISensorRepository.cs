using System.Collections.Generic;
using Parxlab.Entities;
using System.Threading.Tasks;
using Parxlab.Data.Dtos.Sensor;

namespace Parxlab.Repository.Interfaces
{
    public interface ISensorRepository : IRepository<Sensor>
    {
        Task<IEnumerable<SensorDto>> GetAllDto();
        Task<int> StartListening(string sensorId);
        Task<int> StopListening(string sensorId);

    }
}
