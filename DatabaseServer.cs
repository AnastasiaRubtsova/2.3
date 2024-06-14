using Npgsql;
namespace datebook
{
    public class DatabaseServer
    {

        private static NpgsqlConnection? _connection;
        private static string GetConnectionString()
        {
            return @"host=localhost;port=5432;database=databook;username=postgres;password=123";
        }

        public static NpgsqlConnection GetSqlConnection()
        {
            if (_connection is null)
            {
                _connection = new NpgsqlConnection(GetConnectionString());
                _connection.Open();
            }
        
            return _connection;
        }
        
    }
}