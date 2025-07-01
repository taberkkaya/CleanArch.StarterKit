using CleanArch.StarterKit.Infrastructure.Persistence;

namespace CleanArch.StarterKit.Infrastructure.BackgroundJobs;

/// <summary>
/// Background job that cleans up old audit log entries.
/// </summary>
public class AuditLogCleanupJob
{
    private readonly ApplicationDbContext _context;

    public AuditLogCleanupJob(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Deletes audit logs older than 30 days.
    /// </summary>
    public async Task CleanOldLogsAsync()
    {
        var cutoff = DateTime.UtcNow.AddDays(-30);
        var oldLogs = _context.AuditLogs.Where(x => x.Timestamp < cutoff);
        _context.AuditLogs.RemoveRange(oldLogs);
        await _context.SaveChangesAsync();
    }
}
