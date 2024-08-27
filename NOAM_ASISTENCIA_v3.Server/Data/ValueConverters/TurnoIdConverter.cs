using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NOAM_ASISTENCIA_v3.Shared.Helpers.StronglyTypedIds;

namespace NOAM_ASISTENCIA_v3.Server.Data.ValueConverters;

public class TurnoIdConverter() : ValueConverter<TurnoId, int>
    (id => id.Value, intValue => new TurnoId(intValue))
{ }
