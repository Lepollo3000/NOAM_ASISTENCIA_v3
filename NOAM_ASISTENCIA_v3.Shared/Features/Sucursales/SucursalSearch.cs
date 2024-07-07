namespace NOAM_ASISTENCIA_v3.Shared.Features.Sucursales;

public class SucursalSearch : SearchTerm
{
    public int SucursalId { get; set; }
    public string Descripcion { get; set; } = null!;
    public bool? EstaEliminado { get; set; }
}
