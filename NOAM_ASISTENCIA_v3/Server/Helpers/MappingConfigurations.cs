using Ardalis.Result;
using FluentValidation.Results;
using Mapster;
using System.Reflection;

namespace NOAM_ASISTENCIA_v3.Server.Helpers;

public static class MappingConfigurations
{
    public static void RegisterMappingConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig<ValidationFailure, ValidationError>
            .NewConfig()
            .Map(dest => dest.Identifier,
                src => src.PropertyName)
            .Map(dest => dest.ErrorMessage,
                src => src.ErrorMessage)
            .Map(dest => dest.ErrorCode,
                src => src.ErrorCode)
            .Map(dest => dest.Identifier,
                src => ValidationSeverity.Error)
            .TwoWays();

        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
    }
}
