using Fluxera.StronglyTypedId;
using Microsoft.EntityFrameworkCore;
using NOAM_ASISTENCIA_v3.Server.Data.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOAM_ASISTENCIA_v3.Server.Domain;

public sealed class TurnoId(int value) : StronglyTypedId<TurnoId, int>(value) { }

[PrimaryKey(nameof(Id))]
public class Turno : Entidad
{
    public TurnoId Id { get; set; } = null!;
    [MaxLength(1000)]
    public string Descripcion { get; set; } = null!;


    [InverseProperty(nameof(ApplicationUser.Turno))]
    public virtual ICollection<ApplicationUser> Usuarios { get; set; } = null!;
}
