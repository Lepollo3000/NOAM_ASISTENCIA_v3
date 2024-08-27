using NOAM_ASISTENCIA_v3.Server.Domain;
using NOAM_ASISTENCIA_v3.Shared.Helpers.StronglyTypedIds;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOAM_ASISTENCIA_v3.Server.Data.Abstractions;

public class Entidad
{
    public DateTime FechaUtcAlta { get; set; }
    public DateTime? FechaUtcEdita { get; set; }
    public DateTime? FechaUtcElimina { get; set; }
    public IdentityId UsuarioAltaId { get; set; }
    public IdentityId? UsuarioEditaId { get; set; }
    public IdentityId? UsuarioEliminaId { get; set; }
    public bool EstaEliminado { get; set; } = false;


    [ForeignKey(nameof(UsuarioAltaId))]
    public virtual Usuario UsuarioAlta { get; set; } = null!;
    [ForeignKey(nameof(UsuarioEditaId))]
    public virtual Usuario? UsuarioEdita { get; set; }
    [ForeignKey(nameof(UsuarioEliminaId))]
    public virtual Usuario? UsuarioElimina { get; set; }
}
