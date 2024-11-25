using ShopTARge23.Core.Dto;
using MimeKit;
using ShopTARge23.ApplicationServices.Services;
using Castle.Core.Configuration;
using System.Net.Mail;
using ShopTARge23.Core.ServiceInterface;

namespace ShopTARge23.ApplicationServices.Services
{
    public class EmailServices : IEmailServices
    {
        private readonly IConfiguration _config;

        public void SendEmail(EmailDto dto)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUserName".Value));
            email.To.Add(MailboxAddress.Parse(dto.To));
            email.Subject = dto.Subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = dto.Body,
            };

            //kindlasti kasutada mailkit.net.smtp
            using var smtp = new SmtpClient();

            smtp.Connect(_config.GetSection("EmailHost").Value, 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);


            //siin tuleb valida õige port ja kasutada securesocket optionit
            //autentida
            //saada email
            //vabasta ressurss
        }
    }
}
