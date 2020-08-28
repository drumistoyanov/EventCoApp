using EventCoApp.DataAccessLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EventCoApp.DataAccessLibrary.DataAccess
{
    public class EventCoContext : IdentityDbContext<User, Role, int>
    {
        public EventCoContext(DbContextOptions<EventCoContext> options) : base(options)
        {

        }
        public virtual new DbSet<Role> Roles { get; set; }
        public virtual new DbSet<User> Users { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<UserPermission> UserPermissions { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<EventType> EventTypes { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public override int SaveChanges() => this.SaveChanges(true);
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Needed for Identity models configuration
            base.OnModelCreating(builder);

            //ConfigureUserIdentityRelations(builder);

            var entityTypes = builder.Model.GetEntityTypes();

            // Disable cascade delete
            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
            builder.Entity<IdentityRoleClaim<int>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });
            builder.Entity<Booking>(entity =>
            {
                entity.HasOne<User>(d => d.CreatedBy).WithMany(p => p.Bookings).HasForeignKey(d => d.CreatedById);
                entity.HasOne<Event>(d => d.Event).WithMany(p => p.Bookings).HasForeignKey(d => d.EventId);
                entity.ToTable("Bookings");
            });

            builder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName).HasName("RoleNameIndex");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);

                entity.HasMany(d => d.RolePermissions).WithOne(p => p.Role).HasForeignKey(d => d.RoleId);

                entity.ToTable("Roles");
            });
            builder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail).HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName).HasName("UserNameIndex");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.UserPermissions).WithOne(p => p.User).HasForeignKey(d => d.UserId);
                entity.HasMany(e => e.CreatedEvents).WithOne(e => e.CreatedBy).HasForeignKey(e => e.CreatedById);
                entity.ToTable("Users");
            });
            builder.Entity<IdentityUserClaim<int>>(entity =>
            {
                entity.ToTable("UserClaims");
            });
            builder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.Property(e => e.LoginProvider).HasMaxLength(450);

                entity.Property(e => e.ProviderKey).HasMaxLength(450);

                entity.ToTable("UserLogins");
            });
            builder.Entity<IdentityUserRole<int>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
                entity.ToTable("UserRoles");
            });
            builder.Entity<Event>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
                entity.Property(e => e.When).HasColumnType("datetime");
                entity.HasOne<Location>(d => d.Location).WithMany(p => p.Events).HasForeignKey(d => d.LocationId);
                entity.HasMany<Image>(d => d.Images).WithOne(p => p.Event).HasForeignKey(p => p.EventId);
                entity.HasMany<Ticket>(d => d.Tickets).WithOne(p => p.Event).HasForeignKey(p => p.EventId);
                entity.ToTable("Events");
            });
            builder.Entity<Location>(entity =>
            {
                entity.HasIndex(e => e.Name).HasName("BG_Location").IsUnique();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.HasMany<Event>(d => d.Events).WithOne(p => p.Location).HasForeignKey(p => p.LocationId);

                entity.ToTable("Locations");
            });
            builder.Entity<EventType>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.HasMany(p => p.Events).WithOne(p => p.EventType).HasForeignKey(p => p.EventTypeId);

                entity.ToTable("EventTypes");
            });
            builder.Entity<Permission>(entity =>
            {
                entity.HasIndex(e => e.Name).HasName("BG_Permission").IsUnique();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Group)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();
                entity.ToTable("Permissions");
            });
            builder.Entity<RolePermission>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.PermissionId });

                entity.HasOne(d => d.Permission).WithMany(p => p.RolePermissions).HasForeignKey(d => d.PermissionId);

                entity.HasOne(d => d.Role).WithMany(p => p.RolePermissions).HasForeignKey(d => d.RoleId);
                entity.ToTable("RolePermissions");
            });
            builder.Entity<UserPermission>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.PermissionId });

                entity.HasOne(d => d.Permission).WithMany(p => p.UserPermissions).HasForeignKey(d => d.PermissionId);

                entity.HasOne(d => d.User).WithMany(p => p.UserPermissions).HasForeignKey(d => d.UserId);
                entity.ToTable("UserPermissions");
            });
        }

        //private static void ConfigureUserIdentityRelations(ModelBuilder builder)
        //{

        //    builder.Entity<User>()
        //        .HasMany(e => e.Claims)
        //        .WithOne()
        //        .HasForeignKey(e => e.UserId)
        //        .IsRequired()
        //        .OnDelete(DeleteBehavior.Restrict);

        //    builder.Entity<User>()
        //        .HasMany(e => e.Logins)
        //        .WithOne()
        //        .HasForeignKey(e => e.UserId)
        //        .IsRequired()
        //        .OnDelete(DeleteBehavior.Restrict);

        //    builder.Entity<User>()
        //        .HasMany(e => e.Roles)
        //        .WithOne()
        //        .HasForeignKey(e => e.UserId)
        //        .IsRequired()
        //        .OnDelete(DeleteBehavior.Restrict);
        //    builder.Entity<Event>()
        //        .HasOne(e => e.CreatedBy);
        //    builder.Entity<Event>()
        //        .HasOne(e => e.ModifiedBy);
        //}
    }
}
