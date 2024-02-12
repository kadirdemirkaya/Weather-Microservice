using BuildingBlock.Base.Enums;

namespace BuildingBlock.Base.Configs
{
    public class DatabaseConfig
    {
        public object ConnectionString { get; set; }
        public int RetryCount { get; set; } = 5;
        public DatabaseType DatabaseType { get; set; } = DatabaseType.MsSQL;

        public string? DatabaseName { get; set; } = string.Empty;
        public string TableName { get; set; } = string.Empty;
    }
}
