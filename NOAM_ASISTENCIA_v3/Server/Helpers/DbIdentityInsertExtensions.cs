using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace NOAM_ASISTENCIA_v3.Server.Helpers;

public static class DbIdentityInsertExtensions
{
    #region IDENTITY_INSERT
    public static void EnableIdentityInsert<T>(this DbContext context) => SetIdentityInsert<T>(context, true);
    public static void DisableIdentityInsert<T>(this DbContext context) => SetIdentityInsert<T>(context, false);

    private static void SetIdentityInsert<T>([NotNull] DbContext context, bool enable)
    {
        ArgumentNullException.ThrowIfNull(context);

        var entityType = context.Model.FindEntityType(typeof(T));
        var value = enable ? "ON" : "OFF";

        context.Database.ExecuteSqlAsync($"SET IDENTITY_INSERT {entityType!.GetSchema()}.{entityType!.GetTableName()} {value}");
    }

    public static void SaveChangesWithIdentityInsert<T>([NotNull] this DbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        using var transaction = context.Database.BeginTransaction();

        context.EnableIdentityInsert<T>();
        context.SaveChanges();
        context.DisableIdentityInsert<T>();

        transaction.Commit();
    }
    #endregion 

    #region IDENTITY_INSERT ASYNC
    public static async Task EnableIdentityInsertAsync<T>(this DbContext context) => await SetIdentityInsertAsync<T>(context, true);
    public static async Task DisableIdentityInsertAsync<T>(this DbContext context) => await SetIdentityInsertAsync<T>(context, false);

    private static async Task SetIdentityInsertAsync<T>([NotNull] DbContext context, bool enable)
    {
        ArgumentNullException.ThrowIfNull(context);

        var entityType = context.Model.FindEntityType(typeof(T));
        var value = enable ? "ON" : "OFF";

        await context.Database.ExecuteSqlAsync($"SET IDENTITY_INSERT {entityType!.GetSchema()}.{entityType!.GetTableName()} {value}");
    }

    public static async Task SaveChangesWithIdentityInsertAsync<T>([NotNull] this DbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        await using var transaction = await context.Database.BeginTransactionAsync();

        await context.EnableIdentityInsertAsync<T>();
        await context.SaveChangesAsync();
        await context.DisableIdentityInsertAsync<T>();

        await transaction.CommitAsync();
    }
    #endregion 
}
