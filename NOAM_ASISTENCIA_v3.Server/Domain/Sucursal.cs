using Microsoft.EntityFrameworkCore;
using NOAM_ASISTENCIA_v3.Server.Data.Abstractions;
using NOAM_ASISTENCIA_v3.Server.Data.Schemas;
using NOAM_ASISTENCIA_v3.Shared.Helpers.StronglyTypedIds;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOAM_ASISTENCIA_v3.Server.Domain;

[PrimaryKey(nameof(Id))]
[Table(nameof(Sucursal), Schema = ApplicationSchemas.DefaultSchema)]
public class Sucursal : Entidad
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public SucursalId Id { get; set; }
    [MaxLength(5)]
    public string CodigoId { get; set; } = null!;
    [MaxLength(100)]
    public string Descripcion { get; set; } = null!;


    [InverseProperty(nameof(Asistencia.Sucursal))]
    public virtual ICollection<Asistencia> Asistencias { get; set; } = null!;
}
