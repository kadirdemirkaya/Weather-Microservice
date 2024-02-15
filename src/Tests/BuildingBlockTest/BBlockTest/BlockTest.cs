using Amazon.Auth.AccessControlPolicy.ActionIdentifiers;
using BBlockTest.Aggregate.Product;
using BBlockTest.Aggregate.Weather;
using BBlockTest.Constants;
using BBlockTest.Events.EventHandlers;
using BBlockTest.Events.Events;
using BBlockTest.Models;
using BBlockTest.Models.Data.Weather;
using BuildingBlock.Base.Abstractions;
using BuildingBlock.Base.Configs;
using BuildingBlock.Base.Enums;
using BuildingBlock.Base.Options;
using BuildingBlock.Factory.Factories;
using BuildingBlock.Redis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using RabbitMQ.Client;
using static BBlockTest.Constants.Constant;

namespace BBlockTest
{
    public class BlockTest
    {
        private ServiceCollection services;

        public BlockTest()
        {
            services = new ServiceCollection();
        }

        [SetUp]
        public void Setup()
        {
        }

        #region MongoDb Test
        [Test]
        public async Task test_table_process_for_mongodb()
        {
            var client = new MongoClient(Constant.MongoDbUrl);
            var database = client.GetDatabase(Constant.MongoDbDatabase);
            var collection = database.GetCollection<Location>(Constant.DbTableName);

            var location = new Location()
            {
                Id = ObjectId.GenerateNewId(),
                testStr = "test insert example data"
            };

            collection.InsertOne(location);
        }

        [Test]
        public async Task test_table_repo_process_for_mongodb()
        {
            var client = new MongoClient(Constant.MongoDbUrl);
            var database = client.GetDatabase(Constant.MongoDbDatabase);

            services.AddScoped<IWriteRepository<Location>>(sp =>
            {
                return new BuildingBlock.Mongo.WriteRepository<Location>(database);
            });

            var _sp = services.BuildServiceProvider();

            var _writeRepo = _sp.GetService<IWriteRepository<Location>>();

            var location = new Location()
            {
                Id = ObjectId.GenerateNewId(),
                testStr = "test insert example data 2 !"
            };

            await _writeRepo.CreateAsync(location);
        }

        [Test]
        public async Task test_table_repo_process_for_mongodb2()
        {
            var client = new MongoClient(Constant.MongoDbUrl);
            var database = client.GetDatabase(Constant.MongoDbDatabase);

            services.AddScoped<IReadRepository<Location>>(sp =>
            {
                return new BuildingBlock.Mongo.ReadRepository<Location>(database);
            });

            var _sp = services.BuildServiceProvider();

            var _readRepo = _sp.GetService<IReadRepository<Location>>();

            var allDatas = await _readRepo.GetAllAsync();

            await Console.Out.WriteLineAsync();
        }

        [Test]
        public async Task test_db_repo_for_factory()
        {
            var client = new MongoClient(Constant.MongoDbUrl);
            var database = client.GetDatabase(Constant.MongoDbDatabase);

            services.AddScoped<IReadRepository<Location>>(sp =>
            {
                return RepositoryFactory<Location>.CreateReadRepository(new() { ConnectionString = Constant.MongoDbUrl, DatabaseName = Constant.MongoDbDatabase, DatabaseType = BuildingBlock.Base.Enums.DatabaseType.Mongo, RetryCount = 5, TableName = Constant.DbTableName }, null, sp);
            });

            var _sp = services.BuildServiceProvider();

            var _readRepo = _sp.GetService<IReadRepository<Location>>();

            var allDatas = await _readRepo.GetAllAsync();

            await Console.Out.WriteLineAsync();
        }

        [Test]
        public async Task test_db_repo_for_crud()
        {
            var client = new MongoClient(Constant.MongoDbUrl);
            var database = client.GetDatabase(Constant.MongoDbDatabase);

            await InjectRepoExtensionMongo(services);

            var _sp = services.BuildServiceProvider();

            var _readRepo = _sp.GetService<IReadRepository<Location>>();
            var _writeRepo = _sp.GetService<IWriteRepository<Location>>();

            //var data = await _readRepo.GetByGuidAsync("65b67dcc1c1881a4534610d7",false);
            //var res = _writeRepo.DeleteByIdAsync(data.Id.ToString());

            //var location = new Location()
            //{
            //    Id = ObjectId.Parse("65b6ab04f07a6d229298ff6e"),
            //    testStr = "test UPDATE example data, attention !"
            //};

            //bool res = _writeRepo.UpdateAsync(location);

            //bool res = await _writeRepo.DeleteByIdAsync("65b6ab04f07a6d229298ff6e");

            var allData = await _readRepo.GetAllAsync();

            await Console.Out.WriteLineAsync();
        }

        [Test]
        public async Task test_ddd_domain_test()
        {
            var client = new MongoClient(Constant.MongoDbUrl);
            var database = client.GetDatabase(Constant.MongoDbDatabase);

            await InjectRepoExtensionMongo(services);

            var _sp = services.BuildServiceProvider();

            var _readRepo = _sp.GetService<IReadRepository<BBlockTest.Aggregate.Location, BBlockTest.Aggregate.LocationId>>();
            var _writeRepo = _sp.GetService<IWriteRepository<BBlockTest.Aggregate.Location, BBlockTest.Aggregate.LocationId>>();
            var _unitOfWork = _sp.GetService<IUnitOfWork>();

            #region INSERT
            BBlockTest.Aggregate.Location location = new()
            {
                Id = BBlockTest.Aggregate.LocationId.CreateUnique(),
                testStr = "aggregate test insert"
            };

            await _writeRepo.CreateAsync(location);
            #endregion

            #region GET ALL
            //var datas = await _readRepo.GetAllAsync();
            #endregion

            #region Get By Id 
            //var data = await _readRepo.GetByGuidAsync("65b75f7e354a9d0910026ea1", true);
            #endregion

            #region Get 
            //var data = await _readRepo.GetAsync(l => l.Id == BBlockTest.Aggregate.LocationId.Create(ObjectId.Parse("65b75f7e354a9d0910026ea1")));
            #endregion

            #region Count
            //var count = await _readRepo.CountAsync();
            #endregion

            #region Delete
            //BBlockTest.Aggregate.Location location = new Aggregate.Location()
            //{
            //    Id = BBlockTest.Aggregate.LocationId.Create(ObjectId.Parse("65b75f7e354a9d0910026ea1")),
            //    testStr = ""
            //};
            //bool res = await _writeRepo.DeleteByIdAsync(location);

            //-----------------

            //bool res = await _writeRepo.DeleteByIdAsync("65b7b4a376625c8518f65632");
            #endregion

            #region UnitOfWork
            //BBlockTest.Aggregate.Location location = new Aggregate.Location()
            //{
            //    Id = BBlockTest.Aggregate.LocationId.CreateUnique(),
            //    testStr = "insert test repo"
            //};

            //await _unitOfWork.GetWriteRepository<BBlockTest.Aggregate.Location, BBlockTest.Aggregate.LocationId>().CreateAsync(location);

            var datas = await _unitOfWork.GetReadRepository<BBlockTest.Aggregate.Location, BBlockTest.Aggregate.LocationId>().GetAllAsync();
            #endregion

            await Console.Out.WriteLineAsync();
        }
        private async Task InjectRepoExtensionMongo(ServiceCollection services)
        {
            services.AddScoped<IReadRepository<BBlockTest.Aggregate.Location, BBlockTest.Aggregate.LocationId>>(sp =>
            {
                return RepositoryFactory<BBlockTest.Aggregate.Location, BBlockTest.Aggregate.LocationId>.CreateReadRepository(new() { ConnectionString = Constant.MongoDbUrl, DatabaseName = Constant.MongoDbDatabase, DatabaseType = BuildingBlock.Base.Enums.DatabaseType.Mongo, RetryCount = 5, TableName = Constant.DbTableName }, null, sp);
            });

            services.AddScoped<IWriteRepository<BBlockTest.Aggregate.Location, BBlockTest.Aggregate.LocationId>>(sp =>
            {
                return RepositoryFactory<BBlockTest.Aggregate.Location, BBlockTest.Aggregate.LocationId>.CreateWriteRepository(new() { ConnectionString = Constant.MongoDbUrl, DatabaseName = Constant.MongoDbDatabase, DatabaseType = BuildingBlock.Base.Enums.DatabaseType.Mongo, RetryCount = 5, TableName = Constant.DbTableName }, null, sp);
            });

            services.AddScoped<IUnitOfWork>(sp =>
            {
                return UnitOfWorkFactory.CreateUnitOfWork(new() { ConnectionString = Constant.MongoDbUrl, DatabaseName = Constant.MongoDbDatabase, DatabaseType = BuildingBlock.Base.Enums.DatabaseType.Mongo, RetryCount = 5, TableName = Constant.DbTableName }, null, null, sp, typeof(BlockTest).Name);
            });
        }
        #endregion

        #region Sql Test
        [Test]
        public async Task sql_table_crud_test()
        {
            await InjectDbExtension(services);

            await InjectRepoExtensionSql(services);

            Product product = new()
            {
                Id = ProductId.CreateUnique(),
                ProductName = "test product name"
            };

            var _sp = services.BuildServiceProvider();

            var _unitOfWork = _sp.GetRequiredService<IUnitOfWork>();

            bool _res = await _unitOfWork.GetWriteRepository<Product, ProductId>().CreateAsync(product);

            await _unitOfWork.SaveChangesAsync();
            Console.Out.WriteLine();
        }

        [Test]
        public async Task sql_table_crud_test2()
        {
            await InjectDbExtension(services);

            await InjectRepoExtensionSql(services);

            var _sp = services.BuildServiceProvider();

            var _unitOfWork = _sp.GetRequiredService<IUnitOfWork>();

            var _res = await _unitOfWork.GetReadRepository<Product, ProductId>().GetAllAsync();

            Console.Out.WriteLine();
        }

        private async Task InjectRepoExtensionSql(ServiceCollection services)
        {
            var _sp = services.BuildServiceProvider();

            //var dbContext = _sp.GetRequiredService<ProductDbContext>();

            //services.AddScoped<IReadRepository<Product, ProductId>>(sp =>
            //{
            //    return RepositoryFactory<Product, ProductId>.CreateReadRepository(new() { ConnectionString = Constant.Sql.SqlDbUrl, DatabaseName = Constant.Sql.MongoDbDatabase, DatabaseType = BuildingBlock.Base.Enums.DatabaseType.MsSQL, RetryCount = 5, TableName = Constant.Sql.DbTableName }, dbContext, sp);
            //});
            //services.AddScoped<IWriteRepository<Product, ProductId>>(sp =>
            //{
            //    return RepositoryFactory<Product, ProductId>.CreateWriteRepository(new() { ConnectionString = Constant.Sql.SqlDbUrl, DatabaseName = Constant.Sql.MongoDbDatabase, DatabaseType = BuildingBlock.Base.Enums.DatabaseType.MsSQL, RetryCount = 5, TableName = Constant.Sql.DbTableName }, dbContext, sp);
            //});
            //services.AddScoped<IUnitOfWork>(sp =>
            //{
            //    return UnitOfWorkFactory.CreateUnitOfWork(new() { ConnectionString = Constant.Sql.SqlDbUrl, DatabaseName = Constant.Sql.MongoDbDatabase, DatabaseType = BuildingBlock.Base.Enums.DatabaseType.MsSQL, RetryCount = 5, TableName = Constant.Sql.DbTableName }, dbContext, null, sp, Constant.Sql.DbTableName);
            //});
        }

        private async Task InjectDbExtension(ServiceCollection services)
        {
            //services.AddDbContext<ProductDbContext>(options => options.UseSqlServer(Constant.Sql.SqlDbUrl));
        }
        #endregion

        #region Postgre Test
        [Test]
        public async Task test_postgre_crud_repo()
        {
            await InjectPostgreDbExtension(services);

            await InjectPostgreRepoExtensionSql(services);

            var _sp = services.BuildServiceProvider();

            var _unitOfWork = _sp.GetRequiredService<IUnitOfWork>();

            Weather weather = new()
            {
                Id = WeatherId.CreateUnique(),
                Degree = 9
            };

            var _res = await _unitOfWork.GetWriteRepository<Weather, WeatherId>().CreateAsync(weather);

            await Console.Out.WriteLineAsync();
        }

        private async Task InjectPostgreDbExtension(ServiceCollection services)
        {
            services.AddDbContext<WeatherDbContext>(options => options.UseNpgsql(Constant.Postgre.PostgreDbUrl));
        }

        private async Task InjectPostgreRepoExtensionSql(ServiceCollection services)
        {
            var _sp = services.BuildServiceProvider();

            var dbContext = _sp.GetRequiredService<WeatherDbContext>();

            dbContext.Database.Migrate();

            services.AddScoped<IReadRepository<Weather, WeatherId>>(sp =>
            {
                return RepositoryFactory<Weather, WeatherId>.CreateReadRepository(new() { ConnectionString = Constant.Postgre.PostgreDbUrl, DatabaseName = Constant.Postgre.MongoDbDatabase, DatabaseType = BuildingBlock.Base.Enums.DatabaseType.PostgreSQL, RetryCount = 5, TableName = Constant.Postgre.DbTableName }, dbContext, sp);
            });
            services.AddScoped<IWriteRepository<Weather, WeatherId>>(sp =>
            {
                return RepositoryFactory<Weather, WeatherId>.CreateWriteRepository(new() { ConnectionString = Constant.Postgre.PostgreDbUrl, DatabaseName = Constant.Postgre.MongoDbDatabase, DatabaseType = BuildingBlock.Base.Enums.DatabaseType.PostgreSQL, RetryCount = 5, TableName = Constant.Postgre.DbTableName }, dbContext, sp);
            });
            services.AddScoped<IUnitOfWork>(sp =>
            {
                return UnitOfWorkFactory.CreateUnitOfWork(new() { ConnectionString = Constant.Postgre.PostgreDbUrl, DatabaseName = Constant.Postgre.MongoDbDatabase, DatabaseType = BuildingBlock.Base.Enums.DatabaseType.PostgreSQL, RetryCount = 5, TableName = Constant.Postgre.DbTableName }, dbContext, null, sp, Constant.Postgre.DbTableName);
            });
        }
        #endregion

        #region RabbitMq Test
        [Test]
        public async Task event_on_rabbitmq_test()
        {
            SubscribeEvent(services);

            PublishEvent(services);
        }

        [Test]
        public async Task event_on_rabbitmq_test2()
        {
            SubscribeEvent2(services);

            PublishEvent2(services);
        }

        private void PublishEvent(ServiceCollection services)
        {
            var sp = services.BuildServiceProvider();

            var eventBus = sp.GetRequiredService<IEventBus>();

            eventBus.Publish(new UserCreatedIntegrationEvent("test_name"));
        }


        private void PublishEvent2(ServiceCollection services)
        {
            var sp = services.BuildServiceProvider();

            var eventBus = sp.GetRequiredService<IEventBus>();

            eventBus.Publish(new DataCapturedIntegrationEvent("test_name"));
        }

        private void SubscribeEvent(ServiceCollection services)
        {
            services.AddSingleton<IEventBus>(sp =>
            {
                return EventBusFactory.Create(GetEventConfig(), sp);
            });

            services.AddTransient<UserCreatedIntegrationEventHandler>();

            var sp = services.BuildServiceProvider();

            var eventBus = sp.GetRequiredService<IEventBus>();

            eventBus.Subscribe<UserCreatedIntegrationEvent, UserCreatedIntegrationEventHandler>();
        }

        private void SubscribeEvent2(ServiceCollection services)
        {
            services.AddSingleton<IEventBus>(sp =>
            {
                return EventBusFactory.Create(GetEventConfig(), sp);
            });

            services.AddTransient<DataCapturedIntegrationEventHandler>();

            var sp = services.BuildServiceProvider();

            var eventBus = sp.GetRequiredService<IEventBus>();

            eventBus.Subscribe<DataCapturedIntegrationEvent, DataCapturedIntegrationEventHandler>();
        }

        private EventBusConfig GetEventConfig()
           => new()
           {
               ConnectionRetryCount = 5,
               SubscriberClientAppName = "EventBus.UnitTest",
               DefaultTopicName = "WeatherTopicName",
               EventBusType = EventBusType.RabbitMQ,
               EventNameSuffix = "IntegrationEvent",
               EventBusConnectionString = "amqp://guest:guest@localhost:5672"
           };
        #endregion

        #region Redis Test

        [Test]
        public async Task redis_service_test_crud()
        {
            try
            {
                RedisServiceInject(services);

                var _sp = services.BuildServiceProvider();
                var _redisService = _sp.GetRequiredService<IRedisService<Weather, WeatherId>>();

                var weather1 = new Weather();
                weather1.Degree = 12;
                weather1.Id = WeatherId.CreateUnique();

                _redisService.Add(Redis.Key1, weather1, GetTimeSpan(10));

                var res = _redisService.GetList<Weather>(Redis.Key1);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [Test]
        public async Task redis_repository_test_crud()
        {
            try
            {
                RedisRepositoryInject(services);

                var weather1 = new Weather();
                weather1.Degree = 12;
                weather1.Id = WeatherId.CreateUnique();

                var weather2 = new Weather();
                weather2.Degree = 15;
                weather2.Id = WeatherId.CreateUnique();

                var _sp = services.BuildServiceProvider();
                var _redisRepository = _sp.GetRequiredService<IRedisRepository<Weather, WeatherId>>();

                //_redisRepository.Create(Redis.Key1, weather1, RedisDataType.OnlyLists);
                //_redisRepository.Create(Redis.Key1, weather2, RedisDataType.OnlyLists);

                var res = _redisRepository.GetAll(Redis.Key1, RedisDataType.OnlyLists);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void RedisServiceInject(ServiceCollection services)
        {
            services.AddScoped<IRedisService<Weather, WeatherId>>(sp =>
            {
                return new RedisService<Weather, WeatherId>(GetInMemoryOptions(), sp);
            });
        }

        private void RedisRepositoryInject(ServiceCollection services)
        {
            services.AddScoped<IRedisRepository<Weather, WeatherId>>(sp =>
            {
                return new RedisRepository<Weather, WeatherId>(GetRedisConfig(), sp);
            });
        }

        private TimeSpan GetTimeSpan(int num)
            => TimeSpan.FromMinutes(num);

        private RedisConfig GetRedisConfig()
            => new()
            {
                Connection = Redis.RedisUrl,
                ConnectionRetryCount = 5
            };

        private InMemoryOptions GetInMemoryOptions()
            => new InMemoryOptions()
            {
                Connection = Redis.RedisUrl,
                InMemoryType = Redis.memoryType,
                RetryCount = Redis.RetryCount,
                Password = "",
                Username = ""
            };
        #endregion
    }
}