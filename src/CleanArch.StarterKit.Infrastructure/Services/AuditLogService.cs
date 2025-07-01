using CleanArch.StarterKit.Application.Services;
using CleanArch.StarterKit.Domain.Entities;
using CleanArch.StarterKit.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Text.Json;

namespace CleanArch.StarterKit.Infrastructure.Services;

/// <summary>
/// Service for recording audit logs into the database.
/// </summary>
public class AuditLogService : IAuditLogService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuditLogService(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Writes an audit log entry to the database.
    /// </summary>
    /// <param name="action">The action performed (e.g., Create, Update, Delete).</param>
    /// <param name="table">The name of the table affected.</param>
    /// <param name="oldValues">Optional old values as an object.</param>
    /// <param name="newValues">Optional new values as an object.</param>
    public async Task LogAsync(string action, string table, object? oldValues, object? newValues)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "Anonymous";
        var userName = _httpContextAccessor.HttpContext?.User.Identity?.Name ?? "Anonymous";
        var ip = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();

        var auditLog = new AuditLog
        {
            UserId = userId,
            UserName = userName,
            Action = action,
            TableName = table,
            OldValues = oldValues != null ? JsonSerializer.Serialize(oldValues) : null,
            NewValues = newValues != null ? JsonSerializer.Serialize(newValues) : null,
            IpAddress = ip,
            Timestamp = DateTime.Now
        };

        _dbContext.AuditLogs.Add(auditLog);
        await _dbContext.SaveChangesAsync();
    }
}
