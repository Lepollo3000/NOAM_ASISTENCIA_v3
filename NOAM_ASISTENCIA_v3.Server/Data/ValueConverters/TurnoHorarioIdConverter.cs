using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NOAM_ASISTENCIA_v3.Shared.Helpers.StronglyTypedIds;

namespace NOAM_ASISTENCIA_v3.Server.Data.ValueConverters;

public class TurnoHorarioIdConverter() : ValueConverter<TurnoHorarioId, int>
    (id => id.Value, intValue => new TurnoHorarioId(intValue))
{ }
