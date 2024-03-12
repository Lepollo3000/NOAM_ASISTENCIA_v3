using Microsoft.EntityFrameworkCore;
using NOAM_ASISTENCIA_v3.Server.Data.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOAM_ASISTENCIA_v3.Server.Domain;

[PrimaryKey(nameof(UsuarioId), nameof(SucursalId), nameof(FechaEntrada))]
public class Asistencia : Entidad
{
    public UsuarioRolId UsuarioId { get; set; }
    public SucursalId SucursalId { get; set; }
    public DateTime FechaEntrada { get; set; }
    public DateTime? FechaSalida { get; set; }


    [ForeignKey(nameof(UsuarioId))]
    public virtual ApplicationUser Usuario { get; set; } = null!;
    [ForeignKey(nameof(SucursalId))]
    public virtual Sucursal Sucursal { get; set; } = null!;
}
