using Ardalis.Result;
using NOAM_ASISTENCIA_v3.Shared.Helpers.Extensions;
using System.ComponentModel.DataAnnotations;

namespace NOAM_ASISTENCIA_v3.Shared.Helpers.Errors;

public abstract partial class Errors
{
    public abstract class General
    {
        public enum Descriptions
        {
            [Display(Name = "Error inesperado", Description = "Lo sentimos, ocurrió un error inesperado. Inténtelo de nuevo más tarde o consulte a un administrador.")]
            ErrorInesperado,
            [Display(Name = "Error en validaciones", Description = "Se encontraron conflictos con los valores ingresados.")]
            ErrorValidaciones
        }

        public abstract class OperationErrors
        {
            public static Result ErrorInesperado { get; } = Result.Error(Descriptions.ErrorInesperado.GetDisplayDescription());
            public static Result ErrorValidaciones { get; } = Result.Error(Descriptions.ErrorValidaciones.GetDisplayDescription());
        }
    }
}
