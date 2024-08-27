using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace NOAM_ASISTENCIA_v3.Server.Data.ValueConverters;

public class TimeOnlyConverter() : ValueConverter<TimeOnly, TimeSpan>
    (timeOnly => timeOnly.ToTimeSpan(), timeSpan => TimeOnly.FromTimeSpan(timeSpan))
{ }
