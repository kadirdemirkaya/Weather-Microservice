namespace BuildingBlock.Base.Options
{
    public class RabbitMqOptions
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Uri { get; set; }
        public string RabbitMqUrl { get; set; }
        public int ConnectionRetryCount { get; set; } = 5;
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RetryCount { get; set; } = 5;
    }
}
