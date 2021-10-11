using Microsoft.EntityFrameworkCore;

namespace Parxlab.Entities.Mapping
{
    public static class AllMapping
    {
        public static void AddCustomMapping(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SensorMapping());
        }
    }
}
