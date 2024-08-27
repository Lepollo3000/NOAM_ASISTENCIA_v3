using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NOAM_ASISTENCIA_v3.Shared.Helpers.StronglyTypedIds;

namespace NOAM_ASISTENCIA_v3.Server.Data.ValueConverters;

public class TurnoDiaIdConverter() : ValueConverter<TurnoDiaId, int>
    (id => id.Value, intValue => new TurnoDiaId(intValue))
{ }
