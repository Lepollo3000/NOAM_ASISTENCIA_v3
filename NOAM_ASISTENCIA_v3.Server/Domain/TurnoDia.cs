using Microsoft.EntityFrameworkCore;
using NOAM_ASISTENCIA_v3.Server.Data.Abstractions;
using NOAM_ASISTENCIA_v3.Server.Data.Schemas;
using NOAM_ASISTENCIA_v3.Shared.Helpers.StronglyTypedIds;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOAM_ASISTENCIA_v3.Server.Domain;

[PrimaryKey(nameof(Id))]
[Table(nameof(TurnoDia), Schema = ApplicationSchemas.DefaultSchema)]
public class TurnoDia : Entidad
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public TurnoDiaId Id { get; set; }
    public TurnoId TurnoId { get; set; }
    public DayOfWeek Dia { get; set; }


    [ForeignKey(nameof(TurnoId))]
    public virtual Turno Turno { get; set; } = null!;
}
