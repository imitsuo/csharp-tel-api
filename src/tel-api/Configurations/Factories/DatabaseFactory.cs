using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using tel_api.Domain.Models.Options;

namespace tel_api.Configurations.Factories
{
    public interface IDatabaseFactory 
    {
        IDbConnection Connection();
        Task OpenConnectionAsync();
        void CloseConnection();
        void DisposeConnection();
    }

    public class DatabaseFactory : IDatabaseFactory 
    {
        private readonly SqlConnection _connection;
        private readonly Database _database;
        private SqlTransaction _transaction;
        private bool _isTransactionOpen;

        public DatabaseFactory(IOptions<Database> database)
        {
            _database = database.Value ?? throw new ArgumentNullException(nameof(database));

            var connectionString = $"Data Source={_database.DataSource};Initial Catalog={_database.InitialCatalog};User Id={_database.User};Password={_database.Password}";
            _connection = new SqlConnection(connectionString);
        }

        public IDbConnection Connection()
        {
            return _connection;
        }

        public async Task OpenConnectionAsync()
        {
            var connection = _connection as SqlConnection;

            await connection.OpenAsync();
        }

        public void CloseConnection()
        {
            _connection.Close();
        }

        public void DisposeConnection()
        {
            _connection.Dispose();
        }
    }
}