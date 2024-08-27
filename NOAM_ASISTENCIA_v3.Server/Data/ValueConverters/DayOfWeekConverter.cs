using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace NOAM_ASISTENCIA_v3.Server.Data.ValueConverters;

public class DayOfWeekConverter() : ValueConverter<DayOfWeek, int>
    (id => (int)id, intValue => (DayOfWeek)intValue)
{ }
