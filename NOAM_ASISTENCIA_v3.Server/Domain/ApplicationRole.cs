using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel.DataAnnotations.Schema;

namespace NOAM_ASISTENCIA_v3.Server.Domain;

public class ApplicationRole : IdentityRole<UsuarioRolId>
{
    [NotMapped]
    public class IdConverter() : ValueConverter<UsuarioRolId, int>(id => id.Value, intValue => new UsuarioRolId(intValue)) { }
}
