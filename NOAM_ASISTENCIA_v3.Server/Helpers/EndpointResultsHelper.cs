using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
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
        return result.IsSuccess switch
        {
            false when result.ValidationErrors.Count > 0 =>
                Results.BadRequest(new ProblemDetails
                {
                    Title = Errors.General.ErrorValidaciones,
                    Extensions = new Dictionary<string, object?>
                    {
                        { "errors", result.ValidationErrors.Select(model => model.ErrorMessage) }
                    }
                }),
            true =>
                Results.Ok(result.Value),
            _ =>
                Results.Problem(new ProblemDetails
                {
                    Title = Errors.General.ErrorInesperado,
                    Status = StatusCodes.Status500InternalServerError,
                    Extensions = new Dictionary<string, object?>
                    {
                        { "errors", result.Errors }
                    }
                })
        };
    }
}
