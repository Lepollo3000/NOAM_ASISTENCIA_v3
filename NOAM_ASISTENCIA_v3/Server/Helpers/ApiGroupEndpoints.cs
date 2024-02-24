namespace NOAM_ASISTENCIA_v3.Server.Helpers;

public static class ApiGroupEndpoints
{
    private static readonly string ApiRootRoute = "api";

    private static readonly string UsersRouteGroup = $"{ApiRootRoute}/users";

    private static readonly string AccountsRouteGroup = $"{ApiRootRoute}/accounts";

    public static RouteGroupBuilder GetUsersRouteGroup(this IEndpointRouteBuilder app)
    {
        return app.MapGroup(UsersRouteGroup);
    }

    public static RouteGroupBuilder GetAccountsRouteGroup(this IEndpointRouteBuilder app)
    {
        return app.MapGroup(AccountsRouteGroup);
    }
}
