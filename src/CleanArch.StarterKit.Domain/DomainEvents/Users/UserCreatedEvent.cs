using MediatR;

namespace CleanArch.StarterKit.Domain.DomainEvents.Users;

/// <summary>
/// Triggered after a user is created.
/// </summary>
public record UserCreatedEvent(Guid UserId, string UserName, string Email) : INotification;
