using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOAM_ASISTENCIA_v3.Server.Domain;

[PrimaryKey(nameof(UsuarioId), nameof(SucursalId), nameof(FechaEntrada))]
public class Asistencia
{
    public UsuarioRolId UsuarioId { get; set; } = null!;
    public SucursalId SucursalId { get; set; } = null!;
    public DateTime FechaEntrada { get; set; }
    public DateTime? FechaSalida { get; set; }


    [ForeignKey(nameof(UsuarioId))]
    public virtual ApplicationUser Usuario { get; set; } = null!;
    [ForeignKey(nameof(SucursalId))]
    public virtual Sucursal Sucursal { get; set; } = null!;
}
