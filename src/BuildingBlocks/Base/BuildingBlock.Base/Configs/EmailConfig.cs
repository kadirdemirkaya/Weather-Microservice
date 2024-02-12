using BuildingBlock.Base.Enums;

namespace BuildingBlock.Base.Configs
{
    public class EmailConfig
    {
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public string Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public EmailType EmailType { get; set; } = EmailType.Mail;
    }
}
