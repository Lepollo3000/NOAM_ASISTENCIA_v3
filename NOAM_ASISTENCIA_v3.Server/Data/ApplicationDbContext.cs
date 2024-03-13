using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NOAM_ASISTENCIA_v3.Server.Domain;

namespace NOAM_ASISTENCIA_v3.Server.Data;

public class ApplicationDbContext(DbContextOptions options)
: IdentityDbContext<ApplicationUser, ApplicationRole, UsuarioRolId>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var usuarioIdConverter = new ValueConverter<UsuarioRolId, int>(
            id => id.Value,
            guidValue => new UsuarioRolId(guidValue));
        var sucursalIdConverter = new ValueConverter<SucursalId, int>(
            id => id.Value,
            guidValue => new SucursalId(guidValue));
        var turnoIdConverter = new ValueConverter<TurnoId, int>(
            id => id.Value,
            guidValue => new TurnoId(guidValue));

        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(e => e.Id)
                .HasConversion(usuarioIdConverter)
                .UseIdentityColumn();
        });
        modelBuilder.Entity<ApplicationRole>(entity =>
        {
            entity.Property(e => e.Id)
                .HasConversion(usuarioIdConverter)
                .UseIdentityColumn();
        });
        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.Property(e => e.Id)
                .HasConversion(sucursalIdConverter);
        });
        modelBuilder.Entity<Turno>(entity =>
        {
            entity.Property(e => e.Id)
                .HasConversion(turnoIdConverter);
        });
    }
}

