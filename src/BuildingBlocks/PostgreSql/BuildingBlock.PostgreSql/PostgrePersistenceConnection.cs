﻿using BuildingBlock.Base.Configs;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Npgsql;
using Polly;
using System.Net.Sockets;

namespace BuildingBlock.PostgreSql
{
    public class PostgrePersistenceConnection : IDisposable
    {
        private object lock_object = new object();
        private readonly int RetryCount;
        private bool _disposed;
        private DbContextOptions options;
        private DbContext _dbContext;
        private string _connectionString;
        private readonly IServiceProvider _serviceProvider;

        public PostgrePersistenceConnection(string connectionString, int retryCount)
        {
            RetryCount = retryCount;
            options = new DbContextOptionsBuilder()
                .UseNpgsql(connectionString)
                .Options;
            _connectionString = connectionString;
        }

        public PostgrePersistenceConnection(string connectionString, DbContext dbContext, int retryCount = 5)
        {
            _dbContext = dbContext;
            _connectionString = connectionString;
            RetryCount = retryCount;
            options = new DbContextOptionsBuilder()
                .UseNpgsql(connectionString)
                .Options;
            _connectionString = connectionString;
        }

        public PostgrePersistenceConnection(DatabaseConfig dbConfig, DbContext dbContext, int retryCount = 5, IServiceProvider serviceProvider = null)
        {
            _serviceProvider = serviceProvider;
            _dbContext = dbContext;
            if (dbConfig.ConnectionString != null)
            {
                var connJson = JsonConvert.SerializeObject(dbConfig.ConnectionString, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                });
                _connectionString = connJson;
            }
            RetryCount = retryCount;
            options = new DbContextOptionsBuilder()
                .UseNpgsql(_connectionString)
                .Options;
        }

        public DbContextOptions Options() => new DbContextOptionsBuilder()
             .UseNpgsql(_connectionString)
             .Options;

        public DbContextOptions GetOptions
        {
            get
            {
                if (_dbContext is null || options is null)
                    options = Options();
                return options;
            }
        }

        public DbContext GetContext
        {
            get
            {
                if (_dbContext == null || options is null)
                    _dbContext = new DbContext(options ?? Options());
                return _dbContext;
            }
        }

        public bool IsConnected => _dbContext.Database.CanConnect();

        public void Clear() => _dbContext.Dispose();

        public void Dispose()
        {
            _disposed = true;
            _dbContext.Dispose();
        }

        public bool TryConnect()
        {
            lock (lock_object)
            {
                var policy = Policy.Handle<SocketException>()
                    .Or<PostgresException>()
                    .WaitAndRetry(RetryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                    {
                    }
                );

                policy.Execute(() =>
                {
                    #region
                    //to do
                    #endregion
                });

                if (IsConnected)
                {

                    return true;
                }

                return false;
            }
        }

        private void Connection_StateChange(object sender, System.Data.StateChangeEventArgs e)
        {
            if (_disposed) return;

            if (e.CurrentState == System.Data.ConnectionState.Broken)
                TryConnect();
            if (e.CurrentState == System.Data.ConnectionState.Closed)
                TryConnect();
        }
    }
}
