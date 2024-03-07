using Fluxera.StronglyTypedId.EntityFrameworkCore;
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

        modelBuilder.Entity<ApplicationUser>(entity => entity.UseStronglyTypedId());
        modelBuilder.Entity<ApplicationRole>(entity => entity.UseStronglyTypedId());
        modelBuilder.Entity<Asistencia>(entity => entity.UseStronglyTypedId());
        modelBuilder.Entity<Sucursal>(entity => entity.UseStronglyTypedId());
        modelBuilder.Entity<Turno>(entity => entity.UseStronglyTypedId());
    }
}
