namespace BuildingBlock.Base.Options
{
    public class RedisOptions
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Uri { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConnectionRetryCount { get; set; }
        public string EventNameSuffix { get; set; }
        public string DefaultTopicName { get; set; }
        public string SubscriberClientAppName { get; set; }
    }
}
