using Ardalis.Result;

namespace NOAM_ASISTENCIA_v3.Shared.Helpers.Errors;

public abstract class UserErrors : GeneralErrors
{
    public static readonly Result NoEncontrado = Result.Error("El usuario ingresado no se encontró o no existe.");
    public static readonly Result ErrorCreacion = Result.Error("Usuarios.ErrorCreacion");
    public static readonly Result CredencialesInvalidas = Result.Error("Credenciales inválidas. Verifique que se hayan ingresado correctamente.");
}
