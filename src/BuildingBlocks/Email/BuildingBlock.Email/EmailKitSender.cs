using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Exceptions;
using BuildingBlock.Base.Extensions;
using BuildingBlock.Base.Models;
using BuildingBlock.Base.Options;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Serilog;

namespace BuildingBlock.Email
{
    public class EmailKitSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        private EmailOptions _emailOptions;

        public EmailKitSender(IConfiguration configuration)
        {
            _configuration = configuration;
            _emailOptions = configuration.GetOptions<EmailOptions>(nameof(EmailOptions));
        }
        public async Task SendMessageAsync(string to, string subject, string body, bool isBodyHtml = true, string displayName = "")
        {
            Message message = new Message(new string[] { to }, subject, body);
            SendEmail(message);
        }

        public async Task SendMessageAsync(string[] tos, string subject, string body, bool isBodyHtml = true, string displayName = "")
        {
            Message message = new Message(tos, subject, body);
            SendEmail(message);
        }

        public async Task SendMessageWithImageAsync(string[] tos, string subject, string body, bool isBodyHtml, string imagePath, string displayName = "")
        {
            Message message = new Message(tos, subject, body);
            SendEmail(message);
        }

        public void SendEmail(Message message, string? imagePath = null)
        {
            try
            {
                MimeMessage? mimeMessage = null;
                if (!string.IsNullOrEmpty(imagePath))
                {
                    var bodyB = AddImageToMail(imagePath);
                    mimeMessage = CreateEmailMessage(message, bodyB);
                }
                mimeMessage = CreateEmailMessage(message);
                Send(mimeMessage);
            }
            catch (Exception ex)
            {
                Log.Error("Mail Buildingblock Error : " + ex.Message);
                throw new EmailErrorException(ex.Message);
            }
            finally { }
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            try
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress("email", _emailOptions.From));
                emailMessage.To.AddRange(message.To);
                emailMessage.Subject = message.Subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
                
                return emailMessage;
            }
            catch (Exception ex)
            {
                Log.Error("Mail Buildingblock Error : " + ex.Message);
                throw new EmailErrorException(ex.Message);
            }
            finally { }
        }

        private MimeMessage CreateEmailMessage(Message message, BodyBuilder bodyBuilder)
        {
            try
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress("email", _emailOptions.From));
                emailMessage.To.AddRange(message.To);
                emailMessage.Subject = message.Subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
                emailMessage.Body = bodyBuilder.ToMessageBody();

                return emailMessage;
            }
            catch (Exception ex)
            {
                Log.Error("Mail Buildingblock Erro : " + ex.Message);
                throw new EmailErrorException(ex.Message);
            }
            finally { }
        }

        private BodyBuilder AddImageToMail(string imagePath)
        {
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.Attachments.Add(imagePath);
            return bodyBuilder;
        }

        private void Send(MimeMessage mailMessage)
        {
            using var client = new SmtpClient();
            try
            {
                client.Connect(_emailOptions.SmtpServer, int.Parse(_emailOptions.Port), true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailOptions.Username, _emailOptions.Password);
                client.Send(mailMessage);
            }
            catch(Exception ex)
            {
                Log.Error("Mail Buildingblock Error : " + ex.Message);
                throw new EmailErrorException(ex.Message);
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}
