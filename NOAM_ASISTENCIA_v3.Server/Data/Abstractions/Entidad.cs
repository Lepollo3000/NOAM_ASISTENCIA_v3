using NOAM_ASISTENCIA_v3.Server.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOAM_ASISTENCIA_v3.Server.Data.Abstractions;

public class Entidad
{
    public DateTime FechaUtcAlta { get; set; }
    public DateTime? FechaUtcEdita { get; set; }
    public DateTime? FechaUtcElimina { get; set; }
    public UsuarioRolId UsuarioAltaId { get; set; } = null!;
    public UsuarioRolId? UsuarioEditaId { get; set; }
    public UsuarioRolId? UsuarioEliminaId { get; set; }


    [ForeignKey(nameof(UsuarioAltaId))]
    public ApplicationUser UsuarioAlta { get; set; } = null!;
    [ForeignKey(nameof(UsuarioEditaId))]
    public ApplicationUser? UsuarioEdita { get; set; }
    [ForeignKey(nameof(UsuarioEliminaId))]
    public ApplicationUser? UsuarioElimina { get; set; }
}
