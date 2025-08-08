using Npgsql;
using System.Data;

namespace WebApiTest
{
    public class DataBaseConnection
    {
        private static string connectionString =
        "Host=localhost;Port=5432;Username=postgres;Password=12345;Database=WebAPI_db;";

        public IDbConnection GetConnection()
        {
            return new NpgsqlConnection(connectionString);
        }
    }
}
