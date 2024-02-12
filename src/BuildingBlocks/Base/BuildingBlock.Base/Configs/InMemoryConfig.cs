using BuildingBlock.Base.Enums;

namespace BuildingBlock.Base.Configs
{
    public class InMemoryConfig
    {
        public object Connection { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int RetryCount { get; set; } = 5;

        public InMemoryType InMemoryType { get; set; } = InMemoryType.Redis;
    }
}
