namespace CleanArch.StarterKit.Application.Services;

/// <summary>
/// Service for creating audit log records.
/// </summary>
public interface IAuditLogService
{
    /// <summary>
    /// Adds an audit log entry.
    /// </summary>
    /// <param name="action">E.g., "Create", "Update", "Delete", "Login", "Exception", etc.</param>
    /// <param name="table">Table name (optional)</param>
    /// <param name="oldValues">Old values as JSON (optional)</param>
    /// <param name="newValues">New values as JSON (optional)</param>
    Task LogAsync(string action, string table, object? oldValues, object? newValues);
}
