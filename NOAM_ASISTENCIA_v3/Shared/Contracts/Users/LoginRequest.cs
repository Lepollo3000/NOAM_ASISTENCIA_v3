using System.ComponentModel.DataAnnotations;

namespace NOAM_ASISTENCIA_v3.Shared.Contracts.Users;

public class LoginRequest
{
    [Required]
    public string Username { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
    public bool RememberMe { get; set; }
}
