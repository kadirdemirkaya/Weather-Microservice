using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Exceptions;
using BuildingBlock.Base.Extensions;
using BuildingBlock.Base.Options;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace BuildingBlock.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        private EmailOptions _emailOptions;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
            _emailOptions = configuration.GetOptions<EmailOptions>(nameof(EmailOptions));
        }

        public async Task SendMessageAsync(string to, string subject, string body, bool isBodyHtml, string displayName = "")
        {
            await SendMessageAsync(new[] { to }, subject, body, isBodyHtml, displayName);
        }

        public async Task SendMessageAsync(string[] tos, string subject, string body, bool isBodyHtml = true, string displayName = "")
        {
            MailMessage mail = new();
            SmtpClient smtp = new();
            try
            {
                mail.IsBodyHtml = isBodyHtml;
                foreach (var to in tos)
                    mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;
                mail.From = new(_emailOptions.Username, displayName ?? "", System.Text.Encoding.UTF8);

                smtp.Credentials = new NetworkCredential(_emailOptions.Username, _emailOptions.Password);
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Host = _emailOptions.SmtpServer;
                await smtp.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error("Mail Buildingblock Error : " + ex.Message);
                throw new EmailErrorException(ex.Message);
            }
            finally
            {
                mail.Dispose();
                smtp.Dispose();
            }
        }

        public async Task SendMessageWithImageAsync(string[] tos, string subject, string body, bool isBodyHtml, string imagePath, string displayName = "")
        {
            MailMessage mail = new();
            SmtpClient smtp = new();
            try
            {
                mail.IsBodyHtml = isBodyHtml;
                foreach (var to in tos)
                    mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;
                mail.From = new(_emailOptions.Username, displayName ?? "", System.Text.Encoding.UTF8);
                mail.Attachments.Add(new Attachment(imagePath));

                smtp.Credentials = new NetworkCredential(_emailOptions.Username, _emailOptions.Password);
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Host = _emailOptions.SmtpServer;
                await smtp.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                Serilog.Log.Error("Mail Buildingblock Error : " + ex.Message);
                throw new EmailErrorException(ex.Message);
            }
            finally
            {
                mail.Dispose();
                smtp.Dispose();
            }
        }
    }
}
