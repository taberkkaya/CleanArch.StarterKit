namespace CleanArch.StarterKit.Domain.Entities;
public class AuditLog : BaseEntity<int>
{
    public string? UserId { get; set; }
    public string? UserName { get; set; }
    public string? Action { get; set; }
    public string? TableName { get; set; }
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
    public string? IpAddress { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.Now;
}

