using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NOAM_ASISTENCIA_v3.Server.Domain;

namespace NOAM_ASISTENCIA_v3.Server.Data;

public class ApplicationDbContext(DbContextOptions options)
: IdentityDbContext<ApplicationUser, ApplicationRole, UsuarioRolId>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(e => e.Id)
                .HasConversion<ApplicationUserConverter>()
                .UseIdentityColumn();
        });
        modelBuilder.Entity<ApplicationRole>(entity =>
        {
            entity.Property(e => e.Id)
                .HasConversion<ApplicationRoleConverter>()
                .UseIdentityColumn();
        });

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.Property(e => e.Id)
                .HasConversion<SucursalConverter>();
        });
        modelBuilder.Entity<Turno>(entity =>
        {
            entity.Property(e => e.Id)
                .HasConversion<TurnoConverter>();
        });
    }
}
