using CleanArch.StarterKit.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace CleanArch.StarterKit.Domain.Identity;

public class ApplicationRole : IdentityRole<Guid>, IUserAuditable
{
    [JsonIgnore]
    public virtual ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
    public bool IsDeleted { get; set; }
}
