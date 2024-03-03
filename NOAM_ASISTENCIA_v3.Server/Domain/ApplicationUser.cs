using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace NOAM_ASISTENCIA_v3.Server.Domain;

public class ApplicationUser : IdentityUser<int>
{
    [Required]
    public string Nombre { get; set; } = null!;
    [Required]
    public string Apellido { get; set; } = null!;
    [Required]
    public int IdTurno { get; set; }
    [Required]
    public bool Lockout { get; set; }
    [Required]
    public bool ForgotPassword { get; set; }
}
