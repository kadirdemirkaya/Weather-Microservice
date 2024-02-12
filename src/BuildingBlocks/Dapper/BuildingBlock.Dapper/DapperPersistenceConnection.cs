using BuildingBlock.Base.Abstractions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Polly;
using System.Data;
using System.Net.Sockets;

namespace BuildingBlock.Dapper
{
    public class DapperPersistenceConnection : IDisposable, IDbStrategy
    {
        private DbContext _dbContext;
        private object lock_object = new object();
        private readonly int RetryCount;
        private bool _disposed;
        private string _connectionString;
        private IDbStrategy _dbStrategy;
        private DbContextOptions options;
        private IDbConnection _dbConnection;
        private readonly IConfiguration _configuration;

        public DapperPersistenceConnection(string connectionString, int retryCount = 5, IConfiguration configuration = null)
        {
            options = new DbContextOptionsBuilder()
                .UseSqlServer(connectionString)
                .Options;
            _connectionString = connectionString;
            _dbConnection = GetDapperConnection();
            RetryCount = retryCount;
        }

        public DapperPersistenceConnection(DbContext dbContext, string connectionString, int retryCount = 5, IConfiguration configuration = null)
        {
            _dbContext = dbContext;
            options = new DbContextOptionsBuilder()
              .UseSqlServer(connectionString)
              .Options;
            _connectionString = connectionString;
            SetStrategy();
            _dbConnection = GetDapperConnection();
            RetryCount = retryCount;
        }

        public DbContextOptions Options() => new DbContextOptionsBuilder()
          .UseSqlServer(_connectionString)
          .Options;

        public DbContext GetContext()
        {
            if (_dbContext == null || options is null)
                _dbContext = new DbContext(options ?? Options());
            return _dbContext;
        }

        public void SetStrategy()
        {
            _dbStrategy = new SqlServerStrategy(_connectionString, _configuration);
        }

        public IDbConnection GetConnection()
        {
            return _dbStrategy.GetConnection();
        }

        public IDbConnection GetDapperConnection()
        {
            if (_dbContext is null)
                GetContext();
            return GetConnection();
        }

        public bool IsConnected()
        {
            if (_dbConnection is SqlConnection sqlConnection)
                if (sqlConnection.State != ConnectionState.Open)
                    return true;
            return false;
        }

        public bool DbContextIsConnected => _dbContext.Database.CanConnect();

        public void Dispose()
        {
            _dbConnection.Dispose();
            _dbContext.Dispose();
            _connectionString = string.Empty;
        }

        public bool TryConnect()
        {
            lock (lock_object)
            {
                var policy = Polly.Policy.Handle<SocketException>()
                    .Or<SqlException>()
                    .WaitAndRetry(RetryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                    {
                    }
                );

                policy.Execute(() =>
                {
                    using (var transaction = _dbConnection.BeginTransaction())
                    {

                    }
                });

                if (DbContextIsConnected)
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
