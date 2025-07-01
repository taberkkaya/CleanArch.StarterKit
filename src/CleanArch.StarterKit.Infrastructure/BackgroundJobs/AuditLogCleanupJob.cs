using CleanArch.StarterKit.Infrastructure.Persistence;

namespace CleanArch.StarterKit.Infrastructure.BackgroundJobs;
public class AuditLogCleanupJob
{
    private readonly ApplicationDbContext _context;
    public AuditLogCleanupJob(ApplicationDbContext context)
    {
        _context = context;
    }

    // 30 günden eski logları silen örnek
    public async Task CleanOldLogsAsync()
    {
        var cutoff = DateTime.UtcNow.AddDays(-30);
        var oldLogs = _context.AuditLogs.Where(x => x.Timestamp < cutoff);
        _context.AuditLogs.RemoveRange(oldLogs);
        await _context.SaveChangesAsync();
    }
}
