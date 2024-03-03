using System.Security.Claims;

namespace NOAM_ASISTENCIA_v3.Shared.Contracts.Authentication;

public class UserInformation
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = [];

    public ClaimsPrincipal ToClaimsPrincipal() => new(new ClaimsIdentity(new Claim[]
    {
        new (ClaimTypes.Name, Username),
        new (ClaimTypes.Hash, Password),
        new (nameof(FullName), FullName)
    }
    .Concat(Roles.Select(r => new Claim(ClaimTypes.Role, r)).ToArray()), "Blazor School"));

    public static UserInformation FromClaimsPrincipal(ClaimsPrincipal principal) => new()
    {
        Username = principal.FindFirst(ClaimTypes.Name)?.Value ?? "",
        Password = principal.FindFirst(ClaimTypes.Hash)?.Value ?? "",
        FullName = principal.FindFirst(nameof(FullName))?.Value ?? string.Empty,
        Roles = principal.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList()
    };
}
