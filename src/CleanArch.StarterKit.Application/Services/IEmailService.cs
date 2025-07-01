namespace CleanArch.StarterKit.Application.Services;

/// <summary>
/// Service for sending email messages.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Sends an email message to the specified recipient.
    /// </summary>
    /// <param name="to">The recipient email address.</param>
    /// <param name="subject">The subject of the email.</param>
    /// <param name="body">The body content of the email.</param>
    /// <param name="isHtml">Indicates whether the body is HTML. Default is true.</param>
    Task SendAsync(string to, string subject, string body, bool isHtml = true);
}
