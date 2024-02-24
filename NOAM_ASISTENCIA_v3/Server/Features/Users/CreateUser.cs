﻿using Ardalis.Result;
using Carter;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NOAM_ASISTENCIA_v3.Server.Helpers;
using NOAM_ASISTENCIA_v3.Server.Models;
using NOAM_ASISTENCIA_v3.Shared.Contracts.Users;
using NOAM_ASISTENCIA_v3.Shared.Helpers.Services;

namespace NOAM_ASISTENCIA_v3.Server.Features.Users;

public static class CreateUser
{
    public class Query : CreateUserDTO, IRequest<Result> { }

    public class Validator : AbstractValidator<Query>
    {
        public Validator(IUserService userService)
        {
            RuleFor(model => model.Username)
                .CustomAsync(async (username, context, token) =>
                {
                    bool usernameExists = await userService.UsernameExistsAsync(username);

                    if (!usernameExists)
                    {
                        context.AddFailure("Ese nombre de usuario ya existe.");
                    }
                });
        }
    }

    internal sealed class Handler(UserManager<ApplicationUser> userManager, IValidator<CreateUser.Query> validator) : IRequestHandler<Query, Result>
    {
        public async Task<Result> Handle(Query request, CancellationToken cancellationToken)
        {
            var validation = await validator.ValidateAsync(request, cancellationToken);

            if (!validation.IsValid)
            {
                return Result.Invalid(validation.Errors.Adapt<List<ValidationError>>());
            }

            ApplicationUser applicationUser = new()
            {
                UserName = request.Username,
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                IdTurno = request.IdTurno
            };

            IdentityResult createResult = await userManager.CreateAsync(applicationUser, request.Password);

            if (!createResult.Succeeded)
            {
                return Result.Error(createResult.Errors
                    .Select(error => error.Description)
                    .ToArray());
            }

            string token = await userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
            IdentityResult confirmEmailResult = await userManager.ConfirmEmailAsync(applicationUser, token);

            if (!confirmEmailResult.Succeeded)
            {
                return Result.Error(confirmEmailResult.Errors
                    .Select(error => error.Description)
                    .ToArray());
            }

            IdentityResult rolesAddedResult;

            if (request.Roles.Any())
            {
                rolesAddedResult = await userManager.AddToRolesAsync(applicationUser, request.Roles);
            }
            else
            {
                rolesAddedResult = await userManager.AddToRoleAsync(applicationUser, "Intendente");
            }

            if (!rolesAddedResult.Succeeded)
            {
                return Result.Error(rolesAddedResult.Errors
                    .Select(error => error.Description)
                    .ToArray());
            }

            return Result.Success();
        }
    }
}

public class CreateUserEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.GetUsersRouteGroup()
            .MapPost("", async ([FromBody] CreateUserDTO request, IMediator mediator) =>
            {
                var query = request.Adapt<CreateUser.Query>();
                var result = await mediator.Send(query);

                return result.IsSuccess switch
                {
                    true => Results.Ok(result),

                    false when result.ValidationErrors.Count != 0 => Results
                        .BadRequest(result.ValidationErrors
                            .Select(model => model.ErrorMessage)),

                    _ => Results.Problem(
                        statusCode: StatusCodes.Status500InternalServerError,
                        title: "Error inesperado del sistema.",
                        extensions: new Dictionary<string, object?>()
                        {
                            { "error", result.Errors },
                        })
                };
            });
    }
}
