using BuildingBlock.Base.Enums;

namespace BBlockTest.Constants
{
    public static class Constant
    {
        public const string MongoDbUrl = "mongodb://{host}/WeatherDbContext";
        public const string MongoDbDatabase = "WeatherDbContext";
        public const string DbTableName = "Location";

        public static class Sql
        {
            public const string SqlDbUrl = "Server={server};Database=ProductionDbContext;Trusted_Connection=True";
            public const string MongoDbDatabase = "ProductDbContext";
            public const string DbTableName = "Product";
        }

        public static class Postgre
        {
            public const string PostgreDbUrl = "Server={server};port=5432;Database=WeatherDbContext;User Id={user name};Password={password}";
            public const string MongoDbDatabase = "WeatherDbContext";
            public const string DbTableName = "Weather";
        }

        public static class Redis
        {
            public const string RedisUrl = "{host}:6379";
            public const InMemoryType memoryType = InMemoryType.Redis;
            public const int RetryCount = 5;

            public const string Key1 = "Key1";
        }
    }
}
