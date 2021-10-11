using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parxlab.Entities.Mapping
{
    class AllMapping
    {
        public static void AddCustomMapping(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SensorMapping());
        }
    }
}
