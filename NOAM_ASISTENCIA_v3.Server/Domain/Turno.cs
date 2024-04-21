using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NOAM_ASISTENCIA_v3.Server.Data.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOAM_ASISTENCIA_v3.Server.Domain;

public readonly record struct TurnoId(int Value);

public class TurnoConverter() : ValueConverter<TurnoId, int>(id => id.Value, intValue => new TurnoId(intValue)) { }

[PrimaryKey(nameof(Id))]
public class Turno : Entidad
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public TurnoId Id { get; set; }
    [MaxLength(1000)]
    public string Descripcion { get; set; } = null!;


    [InverseProperty(nameof(ApplicationUser.Turno))]
    public virtual ICollection<ApplicationUser> Usuarios { get; set; } = null!;
}
