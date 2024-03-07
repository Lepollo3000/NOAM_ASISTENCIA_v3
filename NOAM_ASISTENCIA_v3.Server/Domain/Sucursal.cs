using Fluxera.StronglyTypedId;
using Microsoft.EntityFrameworkCore;
using NOAM_ASISTENCIA_v3.Server.Data.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOAM_ASISTENCIA_v3.Server.Domain;

public sealed class SucursalId(int value) : StronglyTypedId<SucursalId, int>(value) { }

[PrimaryKey(nameof(Id))]
public class Sucursal : Entidad
{
    public SucursalId Id { get; set; } = null!;
    [MaxLength(5)]
    public string CodigoId { get; set; } = null!;
    [MaxLength(100)]
    public string Descripcion { get; set; } = null!;


    [InverseProperty(nameof(Asistencia.Sucursal))]
    public virtual ICollection<Asistencia> Asistencias { get; set; } = null!;
}
