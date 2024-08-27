using Microsoft.EntityFrameworkCore;
using NOAM_ASISTENCIA_v3.Server.Data.Abstractions;
using NOAM_ASISTENCIA_v3.Server.Data.Schemas;
using NOAM_ASISTENCIA_v3.Shared.Helpers.StronglyTypedIds;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOAM_ASISTENCIA_v3.Server.Domain;

[PrimaryKey(nameof(Id))]
[Table(nameof(Turno), Schema = ApplicationSchemas.DefaultSchema)]
public class Turno : Entidad
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public TurnoId Id { get; set; }
    public TimeOnly HoraInicio { get; set; }
    public TimeOnly HoraFin { get; set; }


    [InverseProperty(nameof(TurnoDia.Turno))]
    public virtual ICollection<TurnoDia> Dias { get; set; } = null!;
    [InverseProperty(nameof(UsuarioTurno.Turno))]
    public virtual ICollection<UsuarioTurno> Usuarios { get; set; } = null!;
}
