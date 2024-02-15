using BuildingBlock.Base.Enums;

namespace BBlockTest.Constants
{
    public static class Constant
    {
        public const string MongoDbUrl = "mongodb://127.0.0.1:27017/WeatherDbContext";
        //public const string MongoDbUrl = "mongodb://localhost:18005/WeatherDbContext";
        public const string MongoDbDatabase = "WeatherDbContext";
        public const string DbTableName = "Location";

        public static class Sql
        {
            public const string SqlDbUrl = "Server=(localdb)\\MSSQLLocalDB;Database=ProductionDbContext;Trusted_Connection=True";
            public const string MongoDbDatabase = "ProductDbContext";
            public const string DbTableName = "Product";
        }

        public static class Postgre
        {
            public const string PostgreDbUrl = "Server=localhost;port=5432;Database=WeatherDbContext;User Id=postgres;Password=123";
            public const string MongoDbDatabase = "WeatherDbContext";
            public const string DbTableName = "Weather";
        }

        public static class Redis
        {
            public const string RedisUrl = "localhost:6379";
            public const InMemoryType memoryType = InMemoryType.Redis;
            public const int RetryCount = 5;

            public const string Key1 = "Key1";
        }
    }
}
