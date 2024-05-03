using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using NOAM_ASISTENCIA_v3.Shared.Helpers.Errors;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace NOAM_ASISTENCIA_v3.Server.Helpers;

public static class EndpointResultsHelper
{
    /// <summary>
    /// Transforma el resultado dado por la solicitud en un resultado de HTTP
    /// </summary>
    /// <returns>Un resultado HTTP de tipo IResult</returns>
    public static IResult ToEndpointResult<T>(this Result<T> result)
    {
        bool hayErrores = !result.IsSuccess
           && result.ValidationErrors.Any();

        return result.IsSuccess switch
        {
            true => Results.Ok(result.Value),

            false when hayErrores => Results.BadRequest(new ProblemDetails
            {
                Title = Errors.General.Descriptions.ErrorValidaciones.GetDisplayName(),
                Extensions = new Dictionary<string, object?>
                {
                    { "errors", result.ValidationErrors.Select(model => model.ErrorMessage) }
                }
            }),

            _ => Results.Problem(new ProblemDetails
            {
                Title = Errors.General.Descriptions.ErrorInesperado.GetDisplayName(),
                Status = StatusCodes.Status500InternalServerError,
                Extensions = new Dictionary<string, object?>
                {
                    { "errors", result.Errors }
                }
            })
        };
    }
}
