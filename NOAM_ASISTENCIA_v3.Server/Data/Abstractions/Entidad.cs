using NOAM_ASISTENCIA_v3.Server.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOAM_ASISTENCIA_v3.Server.Data.Abstractions;

public class Entidad
{
    public DateTime FechaUtcAlta { get; set; }
    public DateTime? FechaUtcEdita { get; set; }
    public DateTime? FechaUtcElimina { get; set; }
    public UsuarioRolId UsuarioAltaId { get; set; }
    public UsuarioRolId? UsuarioEditaId { get; set; }
    public UsuarioRolId? UsuarioEliminaId { get; set; }


    [ForeignKey(nameof(UsuarioAltaId))]
    public virtual ApplicationUser UsuarioAlta { get; set; } = null!;
    [ForeignKey(nameof(UsuarioEditaId))]
    public virtual ApplicationUser? UsuarioEdita { get; set; }
    [ForeignKey(nameof(UsuarioEliminaId))]
    public virtual ApplicationUser? UsuarioElimina { get; set; }
}
