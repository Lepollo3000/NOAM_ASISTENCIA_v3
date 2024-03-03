using Ardalis.Result;

namespace NOAM_ASISTENCIA_v3.Shared.Helpers.Errors;

public abstract partial class Errors
{
    public abstract class General
    {
        public static string ErrorInesperado { get; } = "Lo sentimos, ocurrió un error inesperado. Inténtelo de nuevo más tarde o consulte a un administrador.";
        public static string ErrorValidaciones { get; } = "Se encontraron conflictos con los valores ingresados.";

        public abstract class OperationErrors
        {
            public static Result ErrorInesperado { get; } = Result.Error(General.ErrorInesperado);
            public static Result ErrorValidaciones { get; } = Result.Error(General.ErrorValidaciones);
        }
    }
}
