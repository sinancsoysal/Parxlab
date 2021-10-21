using Microsoft.EntityFrameworkCore;
using Parxlab.Entities.Identity;

namespace Parxlab.Entities.Mapping
{
    public static class IdentityMapping
    {
        public static void AddCustomIdentityMappings(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<UserRole>().ToTable("UserRole");
            modelBuilder.Entity<RoleClaim>().ToTable("RoleClaim");
            modelBuilder.Entity<UserClaim>().ToTable("UserClaim");
            //modelBuilder.Entity<User>().Property(e => e.Id).HasConversion(new ValueConverter<string, Guid>(
            //    v => new Guid(v),
            //    v => v.ToString()));
            //modelBuilder.Entity<Role>().Property(e => e.Id).HasConversion(new ValueConverter<string, Guid>(
            //    v => new Guid(v),
            //    v => v.ToString()));
           // modelBuilder.Entity<RoleClaim>().Property(e => e.Id).HasConversion(new UlidToStringConverter());
           // modelBuilder.Entity<UserClaim>().Property(e => e.Id).HasConversion(new UlidToStringConverter());
            modelBuilder.Entity<UserRole>()
                .HasOne(userRole => userRole.Role)
                .WithMany(role => role.Users).HasForeignKey(r => r.RoleId);

            modelBuilder.Entity<UserRole>()
               .HasOne(userRole => userRole.User)
               .WithMany(role => role.Roles).HasForeignKey(r => r.UserId);

            modelBuilder.Entity<RoleClaim>()
                 .HasOne(roleclaim => roleclaim.Role)
                 .WithMany(claim => claim.Claims).HasForeignKey(c => c.RoleId);

            modelBuilder.Entity<UserClaim>()
                .HasOne(userClaim => userClaim.User)
                .WithMany(claim => claim.Claims).HasForeignKey(c => c.UserId);
        }
    }

}