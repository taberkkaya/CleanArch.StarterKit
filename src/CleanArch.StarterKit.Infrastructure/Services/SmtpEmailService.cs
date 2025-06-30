namespace CleanArch.StarterKit.Infrastructure.Services;

using CleanArch.StarterKit.Application.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class SmtpEmailService(IConfiguration configuration) : IEmailService
{

    public async Task SendAsync(string to, string subject, string body, bool isHtml = true)
    {
        var emailConfig = configuration.GetSection("Email");

        using var client = new SmtpClient(emailConfig["Host"], int.Parse(emailConfig["Port"] ?? string.Empty))
        {
            Credentials = new NetworkCredential(emailConfig["UserName"], emailConfig["Password"]),
            EnableSsl = bool.Parse(emailConfig["EnableSsl"] ?? string.Empty)
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(emailConfig["From"] ?? string.Empty),
            Subject = subject,
            Body = body,
            IsBodyHtml = isHtml
        };
        mailMessage.To.Add(to);

        await client.SendMailAsync(mailMessage);
    }
}

