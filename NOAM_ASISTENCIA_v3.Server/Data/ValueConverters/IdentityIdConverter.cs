using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NOAM_ASISTENCIA_v3.Shared.Helpers.StronglyTypedIds;

namespace NOAM_ASISTENCIA_v3.Server.Data.ValueConverters;

public class IdentityIdConverter() : ValueConverter<IdentityId, int>
        (id => id.Value, intValue => new IdentityId(intValue))
{ }
