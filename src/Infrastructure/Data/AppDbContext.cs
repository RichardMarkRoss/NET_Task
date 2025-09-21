using Microsoft.EntityFrameworkCore;
using UG = UserGroup.Domain.Entities;


namespace UserGroup.Infrastructure.Data;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


    public DbSet<UG.User> Users => Set<UG.User>();
    public DbSet<UG.Group> Groups => Set<UG.Group>();
    public DbSet<UG.Permission> Permissions => Set<UG.Permission>();
    public DbSet<UG.UserGroupMembership> UserGroupMemberships => Set<UG.UserGroupMembership>();
    public DbSet<UG.GroupPermission> GroupPermissions => Set<UG.GroupPermission>();



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // User
        modelBuilder.Entity<UG.User>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Username).HasMaxLength(64).IsRequired();
            e.Property(x => x.Email).HasMaxLength(256).IsRequired();
            e.Property(x => x.FirstName).HasMaxLength(128);
            e.Property(x => x.LastName).HasMaxLength(128);
            e.Property(x => x.RowVersion).IsRowVersion();
            e.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            e.Property(x => x.UpdatedAt);


            e.HasIndex(x => x.Username).IsUnique();
            e.HasIndex(x => x.Email).IsUnique();
        });


        // Group
        modelBuilder.Entity<UG.Group>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).HasMaxLength(128).IsRequired();
            e.Property(x => x.Description).HasMaxLength(512);
            e.Property(x => x.RowVersion).IsRowVersion();
            e.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");


            e.HasIndex(x => x.Name).IsUnique();
        });


        // Permission
        modelBuilder.Entity<UG.Permission>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).HasMaxLength(128).IsRequired();
            e.Property(x => x.Code).HasMaxLength(64);


            e.HasIndex(x => x.Name).IsUnique();
            e.HasIndex(x => x.Code).IsUnique();
        });

        // UserGroup (many-to-many, composite key)
        modelBuilder.Entity<UG.UserGroupMembership>(e =>
        {
            e.HasKey(x => new { x.UserId, x.GroupId });
            e.Property(x => x.Role).HasMaxLength(64);
            e.Property(x => x.AddedAt).HasDefaultValueSql("GETUTCDATE()");

            e.HasOne(x => x.User)
                .WithMany(u => u.UserGroupMemberships)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(x => x.Group)
                .WithMany(g => g.UserGroupMemberships)
                .HasForeignKey(x => x.GroupId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // GroupPermission (many-to-many, composite key)
        modelBuilder.Entity<UG.GroupPermission>(e =>
        {
            e.HasKey(x => new { x.GroupId, x.PermissionId });
            e.Property(x => x.GrantedAt).HasDefaultValueSql("GETUTCDATE()");

            e.HasOne(x => x.Group)
                .WithMany(g => g.GroupPermissions)
                .HasForeignKey(x => x.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(x => x.Permission)
                .WithMany(p => p.GroupPermissions)
                .HasForeignKey(x => x.PermissionId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        var adminsId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var usersId = Guid.Parse("22222222-2222-2222-2222-222222222222");

        var p1 = new UG.Permission { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Level 1", Code = "L1" };
        var p2 = new UG.Permission { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), Name = "Level 2", Code = "L2" };
        var p3 = new UG.Permission { Id = Guid.Parse("55555555-5555-5555-5555-555555555555"), Name = "Level 3", Code = "L3" };

        modelBuilder.Entity<UG.Group>().HasData(
            new UG.Group { Id = adminsId, Name = "Admins", Description = "System administrators" },
            new UG.Group { Id = usersId, Name = "Users", Description = "Regular users" }
        );

        modelBuilder.Entity<UG.Permission>().HasData(p1, p2, p3);

        modelBuilder.Entity<UG.GroupPermission>().HasData(
            new UG.GroupPermission { GroupId = adminsId, PermissionId = p1.Id },
            new UG.GroupPermission { GroupId = adminsId, PermissionId = p2.Id },
            new UG.GroupPermission { GroupId = adminsId, PermissionId = p3.Id },
            new UG.GroupPermission { GroupId = usersId, PermissionId = p1.Id }
        );
    }
}