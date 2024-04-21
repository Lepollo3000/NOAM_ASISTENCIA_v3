using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace NOAM_ASISTENCIA_v3.Server.Domain;

public class ApplicationRoleConverter() : ValueConverter<UsuarioRolId, int>(id => id.Value, intValue => new UsuarioRolId(intValue)) { }

public class ApplicationRole : IdentityRole<UsuarioRolId> { }
