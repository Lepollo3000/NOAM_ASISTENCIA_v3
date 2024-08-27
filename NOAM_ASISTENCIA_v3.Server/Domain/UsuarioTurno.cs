using Microsoft.EntityFrameworkCore;
using NOAM_ASISTENCIA_v3.Server.Data.Abstractions;
using NOAM_ASISTENCIA_v3.Server.Data.Schemas;
using NOAM_ASISTENCIA_v3.Shared.Helpers.StronglyTypedIds;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOAM_ASISTENCIA_v3.Server.Domain;

[PrimaryKey(nameof(TurnoId), nameof(UsuarioId))]
[Table(nameof(UsuarioTurno), Schema = ApplicationSchemas.DefaultSchema)]
public class UsuarioTurno : Entidad
{
    public TurnoId TurnoId { get; set; }
    public IdentityId UsuarioId { get; set; }


    [ForeignKey(nameof(TurnoId))]
    public virtual Turno Turno { get; set; } = null!;
    [ForeignKey(nameof(UsuarioId))]
    public virtual Usuario Usuario { get; set; } = null!;
}
