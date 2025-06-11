using CleanArch.StarterKit.Domain.Abstractions;
using Microsoft.AspNetCore.Identity;
using System;

namespace CleanArch.StarterKit.Domain.Entities;

/// <summary>
/// Application role with audit information.
/// </summary>
public class AppRole : IdentityRole<Guid>, IAuditableEntity
{
    public DateTime CreatedDate { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }
    public Guid? LastModifiedBy { get; set; }
}

