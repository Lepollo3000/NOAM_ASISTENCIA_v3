namespace NOAM_ASISTENCIA_v3.Client.Helpers.Users;

public record CustomUserClaims(string Name = null!, string Email = null!, string[] Roles = null!);
