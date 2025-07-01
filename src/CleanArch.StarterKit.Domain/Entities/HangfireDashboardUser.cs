namespace CleanArch.StarterKit.Domain.Entities;
// Domain/Entities/HangfireDashboardUser.cs
public class HangfireDashboardUser
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UserName { get; set; } = default!;
    public string PasswordHash { get; set; } = default!; // Hash'li sakla!
    public bool IsActive { get; set; } = true;
}
