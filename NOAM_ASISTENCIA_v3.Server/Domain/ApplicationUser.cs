using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOAM_ASISTENCIA_v3.Server.Domain;

public readonly record struct UsuarioRolId(int Value);

public class ApplicationUser : IdentityUser<UsuarioRolId>
{
    [MaxLength(500)]
    public string Nombres { get; set; } = null!;
    [MaxLength(500)]
    public string Apellidos { get; set; } = null!;
    public TurnoId? TurnoId { get; set; }
    public bool Lockout { get; set; }
    public bool ForgotPassword { get; set; }


    [ForeignKey(nameof(TurnoId))]
    public virtual Turno? Turno { get; set; }

    [InverseProperty(nameof(Asistencia.Usuario))]
    public virtual ICollection<Asistencia> Asistencias { get; set; } = null!;
}
