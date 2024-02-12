namespace BuildingBlock.Base.Configs
{
    public class RedisConfig
    {
        public object Connection { get; set; }
        public int ConnectionRetryCount { get; set; } = 5;
    }
}
