namespace NOAM_ASISTENCIA_v3.Server.Helpers;

public static class ApiGroupEndpoints
{
    private static readonly string ApiRootRoute = "api";
    private static readonly string UsersRouteGroup = $"{ApiRootRoute}/users";
    private static readonly string AccountsRouteGroup = $"{ApiRootRoute}/accounts";
    private static readonly string SucursalesRouteGroup = $"{ApiRootRoute}/sucursales";

    /// <summary>
    /// Genera un grupo de endpoints para los usuarios.
    /// </summary>
    /// <remarks>Ruta: 'api/users'</remarks>
    /// <returns>Un grupo de endpoints de tipo RouteGroupBuilder</returns>
    public static RouteGroupBuilder GetUsersRouteGroup(this IEndpointRouteBuilder app)
    {
        return app.MapGroup(UsersRouteGroup);
    }

    /// <summary>
    /// Genera un grupo de endpoints para las cuentas de la aplicación.
    /// </summary>
    /// <remarks>Ruta: '<see cref="AccountsRouteGroup"/>'</remarks>
    /// <returns>Un grupo de endpoints de tipo RouteGroupBuilder</returns>
    public static RouteGroupBuilder GetAccountsRouteGroup(this IEndpointRouteBuilder app)
    {
        return app.MapGroup(AccountsRouteGroup);
    }

    /// <summary>
    /// Genera un grupo de endpoints para las sucursales de la aplicación.
    /// </summary>
    /// <remarks>Ruta: '<see cref="AccountsRouteGroup"/>'</remarks>
    /// <returns>Un grupo de endpoints de tipo RouteGroupBuilder</returns>
    public static RouteGroupBuilder GetSucursalesRouteGroup(this IEndpointRouteBuilder app)
    {
        return app.MapGroup(SucursalesRouteGroup);
    }
}
