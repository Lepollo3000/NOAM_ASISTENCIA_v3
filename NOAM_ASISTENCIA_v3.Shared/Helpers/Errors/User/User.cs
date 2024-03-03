using Ardalis.Result;

namespace NOAM_ASISTENCIA_v3.Shared.Helpers.Errors;

public abstract partial class Errors
{
    public abstract class User
    {
        public static string NoEncontrado { get; } = "El usuario ingresado no se encontró o no existe.";
        public static string CredencialesInvalidas { get; } = "Credenciales inválidas. Verifique que se hayan ingresado correctamente.";
        
        public abstract class OperationErrors
        {
            public static Result NoEncontrado { get; } = Result.Error(User.NoEncontrado);
            public static Result CredencialesInvalidas { get; } = Result.Error(User.CredencialesInvalidas);
        }
    }
}
