using ApiDisparoEmail.Interface;
using ApiDisparoEmail.Model;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ApiDisparoEmail.Service
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _mailSettings;
        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _mailSettings = emailSettings.Value;
        }

        public async Task EnvioEmail(Email solicitacaoEmail)
        {
            MailMessage message = new MailMessage();
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();

            message.From = new MailAddress(_mailSettings.Mail, _mailSettings.DisplayName);
            message.To.Add(new MailAddress(solicitacaoEmail.EmailDestino));
            message.Subject = solicitacaoEmail.Assunto;

            if (solicitacaoEmail.Anexo != null)
            {
                foreach (var file in solicitacaoEmail.Anexo)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            Attachment att = new Attachment(new MemoryStream(fileBytes), file.FileName);
                            message.Attachments.Add(att);
                        }
                    }
                }
            }

            message.IsBodyHtml = false;
            message.Body = solicitacaoEmail.CorpoEmail;
            smtp.Port = _mailSettings.Port;
            smtp.Host = _mailSettings.Host;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(_mailSettings.Mail, _mailSettings.Password);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            await smtp.SendMailAsync(message);
        }
    }

}





