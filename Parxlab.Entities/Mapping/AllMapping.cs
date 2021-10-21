using Microsoft.EntityFrameworkCore;

namespace Parxlab.Entities.Mapping
{
    public static class AllMapping
    {
        public static void AddCustomMapping(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ActivityLogMapping());
            modelBuilder.ApplyConfiguration(new InvoiceMapping());
            modelBuilder.ApplyConfiguration(new ParkMapping());
            modelBuilder.ApplyConfiguration(new ParkUsageMapping());
            modelBuilder.ApplyConfiguration(new RefreshTokenMapping());
            modelBuilder.ApplyConfiguration(new ReservedMapping());
            modelBuilder.ApplyConfiguration(new SensorMapping());
        }
    }
}
