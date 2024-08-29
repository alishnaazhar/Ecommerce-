using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Shared.Helpers;

namespace BL.Services.External;

public class EmailService(IConfiguration config)
{


    public async Task SendEmailAsync(string recipient, string subject, string body)
    {
        await SendEmailAsync(new List<string> { recipient }, subject, body);
    }

    public async Task SendEmailAsync(List<string> recipients, string subject, string body)
    {
        var email = new MimeMessage();
        email.Sender = MailboxAddress.Parse(config["MailSettings:Email"]);

        foreach (var recipient in recipients)
            email.To.Add(MailboxAddress.Parse(recipient));

        email.Subject = subject;

        var builder = new BodyBuilder();
        builder.HtmlBody = body;
        email.Body = builder.ToMessageBody();

        using (var smtp = new SmtpClient())
        {
            await smtp.ConnectAsync(config["MailSettings:Host"], ConversionHelper.ConvertTo<int>(config["MailSettings:Port"]!), SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(config["MailSettings:Email"], config["MailSettings:Password"]);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }

}
