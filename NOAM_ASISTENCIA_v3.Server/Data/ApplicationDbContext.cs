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
                .HasConversion<ApplicationUser.IdConverter>()
                .UseIdentityColumn();
        });

        modelBuilder.Entity<ApplicationRole>(entity =>
        {
            entity.Property(e => e.Id)
                .HasConversion<ApplicationRole.IdConverter>()
                .UseIdentityColumn();
        });

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.Property(e => e.Id)
                .HasConversion<Sucursal.IdConverter>();
        });

        modelBuilder.Entity<Turno>(entity =>
        {
            entity.Property(e => e.Id)
                .HasConversion<Turno.IdConverter>();
        });
    }
}
