using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Parxlab.Entities;
using Parxlab.Entities.Identity;
using Parxlab.Entities.Mapping;

namespace Parxlab.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, IdentityUserLogin<Guid>, RoleClaim, IdentityUserToken<Guid>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<ActivityLog> ActivityLog{ get; set; }
        public virtual DbSet<Invoice> Invoice{ get; set; }
        public virtual DbSet<Park> Park{ get; set; }
        public virtual DbSet<ParkUsage> ParkUsage{ get; set; }
        public virtual DbSet<RefreshToken> RefreshToken { get; set; }
        public virtual DbSet<Reserved> Reserved { get; set; }
        public virtual DbSet<Sensor> Sensor { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("dbo");
            builder.AddCustomIdentityMappings();
            builder.AddCustomMapping();
        }
    }
}
