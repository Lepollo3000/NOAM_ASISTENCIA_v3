using Ardalis.Result;

namespace NOAM_ASISTENCIA_v3.Shared.Helpers.Errors;

public abstract class GeneralErrors
{
    public static readonly Result ErrorInesperado = Result.Error("Lo sentimos, ocurrió un error inesperado. Inténtelo de nuevo más tarde o consulte a un administrador.");
}
