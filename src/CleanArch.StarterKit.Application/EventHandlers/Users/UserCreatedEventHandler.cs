using CleanArch.StarterKit.Application.Services;
using CleanArch.StarterKit.Domain.DomainEvents.Users;
using MediatR;

namespace CleanArch.StarterKit.Application.EventHandlers.Users;

/// <summary>
/// Handles the action after a user is created (e.g., send email).
/// </summary>
public sealed class UserCreatedEventHandler(IEmailService emailService) : INotificationHandler<UserCreatedEvent>
{
    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        await emailService.SendAsync(
            "user@yourdomain.com",
            "New User Registered",
            $"A new user was registered: {notification.UserName} ({notification.Email})",
            true,
            new() { "system@system.com" }
        );
    }
}
