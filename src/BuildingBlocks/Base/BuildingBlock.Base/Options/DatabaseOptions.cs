using BuildingBlock.Base.Enums;

namespace BuildingBlock.Base.Options
{
    public class DatabaseOptions
    {
        public string? ConnectionUrl { get; set; }
        public int? RetryCount { get; set; } = 5;
        public DatabaseType DatabaseType { get; set; } = DatabaseType.MsSQL;

        public string? DatabaseName { get; set; } = string.Empty;
        public string? TableName { get; set; } = string.Empty;
    }
}
