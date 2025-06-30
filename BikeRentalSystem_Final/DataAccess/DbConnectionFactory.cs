using Microsoft.Data.SqlClient;
using System.Data;

namespace BikeRentalSystem_Final.DataAccess
{
    public class DbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection Create() => new SqlConnection(_connectionString);
    }
}