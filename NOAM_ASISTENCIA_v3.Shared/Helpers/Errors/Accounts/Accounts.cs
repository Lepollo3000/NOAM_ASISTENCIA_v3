using Ardalis.Result;
using NOAM_ASISTENCIA_v3.Shared.Helpers.Extensions;
using System.ComponentModel.DataAnnotations;

namespace NOAM_ASISTENCIA_v3.Shared.Helpers.Errors;

public abstract partial class Errors
{
    public abstract class Accounts
    {
        public enum Descriptions
        {
            [Display(Name = "Usuario no encontrado", Description = "El usuario ingresado no se encontró o no existe.")]
            NoEncontrado,
            [Display(Name = "Credenciales inválidas", Description = "Credenciales inválidas. Verifique que se hayan ingresado correctamente.")]
            CredencialesInvalidas
        }

        public static Result NoEncontrado { get; } = Result.Error(Descriptions.NoEncontrado.GetDisplayDescription());
        public static Result CredencialesInvalidas { get; } = Result.Error(Descriptions.CredencialesInvalidas.GetDisplayDescription());
    }
}
