namespace BuildingBlock.Base.Configs
{
    public class MsSqlConfig
    {
        public object ConnectionString { get; set; }
        public int ConnectionRetryCount { get; set; } = 5;
        public string Username { get; set; }
        public string Password { get; set; }
        public bool TrustedConnection { get; set; } = true;
    }
}
