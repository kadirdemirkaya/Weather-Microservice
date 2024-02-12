using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Configs;
using BuildingBlock.Base.Enums;
using BuildingBlock.Email;
using Microsoft.Extensions.Configuration;

namespace BuildingBlock.Factory.Factories
{
    public static class EmailFactory
    {
        public static IEmailSender Create(EmailConfig config, IServiceProvider sp,IConfiguration configuration)
        {
            return config.EmailType switch
            {
                EmailType.Mail => new EmailSender(configuration),
                EmailType.MailKit => new EmailKitSender(configuration),
                _ => new EmailSender(configuration)
            }; ;
        }
    }
}
