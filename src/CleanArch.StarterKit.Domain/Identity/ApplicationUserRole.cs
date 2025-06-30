using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace CleanArch.StarterKit.Domain.Identity;
public class ApplicationUserRole : IdentityUserRole<Guid>
{
    [JsonIgnore]
    public virtual ApplicationUser User { get; set; }
    public virtual ApplicationRole Role { get; set; }
}

