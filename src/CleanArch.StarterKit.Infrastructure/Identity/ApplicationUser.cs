using Microsoft.AspNetCore.Identity;
using CleanArch.StarterKit.Domain.Entities;

namespace CleanArch.StarterKit.Infrastructure.Identity;

public class ApplicationUser : IdentityUser<Guid>, IUserAuditable
{
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
    public bool IsDeleted { get; set; }
}
