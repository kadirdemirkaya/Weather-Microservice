using BuildingBlock.Base.Abstractions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Text.RegularExpressions;

namespace BuildingBlock.Dapper
{
    public class SqlServerStrategy : IDbStrategy
    {
        private string _connectionString;
        private readonly IConfiguration _configuration;

        public SqlServerStrategy(string connectionString,IConfiguration configuration)
        {
            _connectionString = connectionString;
            _configuration = configuration;
        }

        public IDbConnection GetConnection()
            => new SqlConnection(_connectionString ?? _configuration["DbConnectionString:ConnectionUrl"]);


        private string AddQuotesIfMissing(string connectionString)
        {
            if (!connectionString.StartsWith("\""))
                connectionString = "\"" + connectionString;

            if (!connectionString.EndsWith("\""))
                connectionString += "\"";

            return connectionString;
        }

        public string RemoveQuotesIfPresent(string connectionString)
        {
            if (connectionString.StartsWith("\""))
                connectionString = connectionString.Substring(1);

            if (connectionString.EndsWith("\""))
                connectionString = connectionString.Substring(0, connectionString.Length - 1);

            return connectionString;
        }

        private string FormatConnectionString(string originalConnectionString)
        {
            string serverValue = ExtractValue(originalConnectionString, "Server");
            string databaseValue = ExtractValue(originalConnectionString, "Database");
            string newConnectionString = $"Server={serverValue};Database={databaseValue};Trusted_Connection=True;";

            return newConnectionString;
        }

        private string ExtractValue(string connectionString, string key)
        {
            string pattern = $"{key}=([^;]+);";

            Match match = Regex.Match(connectionString, pattern);

            if (match.Success)
                return match.Groups[1].Value;
            else
                return string.Empty;
        }
        private string EscapeBackslashes(string input)
            => input.Replace(@"\", @"\");
    }
}
