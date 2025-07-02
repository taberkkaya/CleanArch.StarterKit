using CleanArch.StarterKit.Domain.Entities.Identity;
using MediatR;

namespace CleanArch.StarterKit.Domain.DomainEvents.Users;

/// <summary>
/// Triggered after a user is created.
/// </summary>
public record UserCreatedEvent(ApplicationUser User) : INotification;
