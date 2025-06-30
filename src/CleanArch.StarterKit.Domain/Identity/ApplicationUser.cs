using CleanArch.StarterKit.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArch.StarterKit.Domain.Identity;

public class ApplicationUser : IdentityUser<Guid>, IUserAuditable
{
    public virtual ICollection<ApplicationRole> Roles { get; set; } = new List<ApplicationRole>();
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
    public bool IsDeleted { get; set; }
}
