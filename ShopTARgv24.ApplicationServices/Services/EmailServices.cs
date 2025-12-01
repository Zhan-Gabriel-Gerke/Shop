using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using ShopTARgv24.Core.Dto;
using System.IO;
using ShopTARgv24.Core.ServiceInterface;

namespace ShopTARgv24.ApplicationServices.Services;

public class EmailServices : IEmailService
{
    private readonly IConfiguration _config;

    public EmailServices
    (
        IConfiguration config
    )
    {
        _config = config;
    }
    
    public void SendEmail(EmailDto dto)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUserName").Value));
        email.To.Add(MailboxAddress.Parse(dto.To));
        email.Subject = dto.Subject;

        var builder = new BodyBuilder
        {
            HtmlBody = dto.Body
        };
        
        if (dto.Attachments != null)
        {
            foreach (var file in dto.Attachments)
            {
                if (file.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        file.CopyTo(stream);
                        stream.Position = 0;
                        builder.Attachments.Add(file.FileName, stream.ToArray());
                    }
                }
            }
        }
        
        email.Body = builder.ToMessageBody();

        using var smtp = new SmtpClient();

        smtp.Connect(_config.GetSection("EmailHost").Value, 587, MailKit.Security.SecureSocketOptions.StartTls);
        smtp.Authenticate(_config.GetSection("EmailUserName").Value, _config.GetSection("EmailPassword").Value);
        smtp.Send(email);
        smtp.Disconnect(true);
    }
}