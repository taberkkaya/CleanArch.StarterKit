namespace CleanArch.StarterKit.Application.Services;
public interface IAuditLogService
{
    /// <summary>
    /// Audit log kaydı ekler.
    /// </summary>
    /// <param name="action">Örn: "Create", "Update", "Delete", "Login", "Exception" vs.</param>
    /// <param name="tableName">Tablo adı (isteğe bağlı)</param>
    /// <param name="oldValues">Json olarak eski değerler (isteğe bağlı)</param>
    /// <param name="newValues">Json olarak yeni değerler (isteğe bağlı)</param>
    /// <returns></returns>
    Task LogAsync(string action, string table, object? oldValues, object? newValues);
}
