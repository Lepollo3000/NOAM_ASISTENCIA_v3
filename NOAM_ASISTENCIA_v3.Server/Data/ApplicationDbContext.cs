using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NOAM_ASISTENCIA_v3.Server.Data.ValueConverters;
using NOAM_ASISTENCIA_v3.Server.Domain;
using NOAM_ASISTENCIA_v3.Shared.Helpers.StronglyTypedIds;

namespace NOAM_ASISTENCIA_v3.Server.Data;

public class ApplicationDbContext(DbContextOptions options)
: IdentityDbContext<Usuario, Rol, IdentityId>(options)
{
    public virtual DbSet<Sucursal> Sucursales { get; set; }
    public virtual DbSet<Asistencia> Asistencias { get; set; }
    public virtual DbSet<Turno> Turnos { get; set; }
    public virtual DbSet<TurnoDia> TurnoDias { get; set; }
    public virtual DbSet<UsuarioTurno> UsuarioTurnos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.Property(e => e.Id)
                .HasConversion<IdentityIdConverter>()
                .UseIdentityColumn();
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.Property(e => e.Id)
                .HasConversion<IdentityIdConverter>()
                .UseIdentityColumn();
        });

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.Property(e => e.Id)
                .HasConversion<SucursalIdConverter>();
        });

        modelBuilder.Entity<Turno>(entity =>
        {
            entity.Property(e => e.Id)
                .HasConversion<TurnoIdConverter>();
        });

        modelBuilder.Entity<TurnoDia>(entity =>
        {
            entity.Property(e => e.Id)
                .HasConversion<TurnoDiaIdConverter>();

            entity.Property(e => e.Dia)
                .HasConversion<DayOfWeekConverter>();
        });
    }
}
