namespace WebApp.Services;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
public class EmailSender : IEmailSender{
    MailSettings settings;
    public EmailSender(IOptions<MailSettings> options) 
        =>  settings = options.Value;
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        using SmtpClient client = new SmtpClient{
            Host = settings.Host,
            EnableSsl = true,
            Port = settings.Port,
            Credentials = new NetworkCredential(settings.Email, settings.Password),
            UseDefaultCredentials = false
        };
        var message = new MailMessage{
            From = new MailAddress(settings.Email),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true
        };
        message.To.Add(new MailAddress(email));
        await client.SendMailAsync(message);
    }
}