using Microsoft.AspNetCore.Identity;
using NOAM_ASISTENCIA_v3.Server.Data.Schemas;
using NOAM_ASISTENCIA_v3.Shared.Helpers.StronglyTypedIds;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOAM_ASISTENCIA_v3.Server.Domain;

public class Usuario : IdentityUser<IdentityId>
{
    [MaxLength(500)]
    public string Nombres { get; set; } = null!;
    [MaxLength(500)]
    public string Apellidos { get; set; } = null!;
    public bool Lockout { get; set; }
    public bool ForgotPassword { get; set; }


    [InverseProperty(nameof(UsuarioTurno.Usuario))]
    public virtual ICollection<UsuarioTurno> Turnos { get; set; } = null!;
    [InverseProperty(nameof(Asistencia.Usuario))]
    public virtual ICollection<Asistencia> Asistencias { get; set; } = null!;
}
