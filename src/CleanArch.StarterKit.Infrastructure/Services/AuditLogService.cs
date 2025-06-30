using CleanArch.StarterKit.Application.Services;
using CleanArch.StarterKit.Domain.Entities;
using CleanArch.StarterKit.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Text.Json;

namespace CleanArch.StarterKit.Infrastructure.Services;
public class AuditLogService : IAuditLogService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuditLogService(ApplicationDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task LogAsync(string action, string table, object? oldValues, object? newValues)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "Anonymous";
        var userName = _httpContextAccessor.HttpContext?.User.Identity?.Name ?? "Anonymous";
        var ip = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();

        var auditLog = new AuditLog
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            UserName = userName,
            Action = action,
            TableName = table,
            OldValues = oldValues != null ? JsonSerializer.Serialize(oldValues) : null,
            NewValues = newValues != null ? JsonSerializer.Serialize(newValues) : null,
            IpAddress = ip,
            Timestamp = DateTime.UtcNow
        };

        _dbContext.AuditLogs.Add(auditLog);
        await _dbContext.SaveChangesAsync();
    }
}
