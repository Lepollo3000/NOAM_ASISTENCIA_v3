using Ardalis.Result;
using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NOAM_ASISTENCIA_v3.Server.Domain;
using NOAM_ASISTENCIA_v3.Server.Helpers;
using NOAM_ASISTENCIA_v3.Shared.Contracts.Users;
using NOAM_ASISTENCIA_v3.Shared.Helpers.Errors;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NOAM_ASISTENCIA_v3.Server.Features.Accounts;

public static class Login
{
    public class Request : LoginRequest, IRequest<Result<LoginResponse>> { }

    internal sealed class Handler(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IConfiguration configuration, ILogger<Handler> logger) : IRequestHandler<Request, Result<LoginResponse>>
    {
        public async Task<Result<LoginResponse>> Handle(Request request, CancellationToken cancellationToken)
        {
            try
            {
                ApplicationUser? user = await userManager.FindByNameAsync(request.Username);

                if (user == null)
                {
                    return Errors.User.OperationErrors.CredencialesInvalidas;
                }

                var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                if (!result.Succeeded)
                {
                    return Errors.User.OperationErrors.CredencialesInvalidas;
                }

                IEnumerable<string> roles = await userManager.GetRolesAsync(user);
                List<Claim> claims = [new Claim(ClaimTypes.Name, request.Username)];

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                IConfigurationSection tokenSection = configuration.GetSection("TokenValidationParameters");

                SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(
                    tokenSection.GetValue<string>("JwtSecurityKey") ?? string.Empty));
                SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);
                DateTime expiryDate = DateTime.Now.AddDays(
                    tokenSection.GetValue<int>("JwtExpiryInDays"));

                JwtSecurityToken token = new(
                    tokenSection["JwtIssuer"],
                    tokenSection["JwtAudience"],
                    claims,
                    expires: expiryDate,
                    signingCredentials: credentials
                );

                LoginResponse modelResult = new(new JwtSecurityTokenHandler().WriteToken(token));

                return Result.Success(modelResult);
            }
            catch (Exception ex)
            {
                logger.LogError("Error al intentar iniciar sesión: {Message}", ex.Message);

                return Errors.General.OperationErrors.ErrorInesperado;
            }
        }
    }
}

public class LoginEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.GetAccountsRouteGroup()
            .MapPost("login", async ([FromBody] LoginRequest request, IMediator mediator) =>
            {
                var query = request.Adapt<Login.Request>();
                var result = await mediator.Send(query);

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
            });
    }
}
