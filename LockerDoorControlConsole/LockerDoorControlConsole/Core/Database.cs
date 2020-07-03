/// <summary>
/// The class containing the Database Connection
/// </summary>

using System;
using MySql.Data.MySqlClient;

namespace LockerDoorControlConsole.Core
{
    public static class Database
    {
        //  Private attribute
        private static MySqlConnection _connection;

        //  Get the Database Connection
        public static MySqlConnection Connection { get { return _connection; } }

        //  Set the Database Connection
        public static void Initialize(string server, string port, string uid, string pw, string db)
        {
            string connectionString = String.Format("SERVER={0};PORT={1};UID={2};PASSWORD={3};DATABASE={4};SSLMODE=NONE", server, port, uid, pw, db);
            _connection = new MySqlConnection(connectionString);
        }

        //  Connect to the Database
        public static bool Connect()
        {
            try
            {
                _connection.Open();
                return true;
            }
            catch (MySqlException)
            {
                return false;
            }
        }

        //  Disconnect from the Database
        public static bool Disconnect()
        {
            try
            {
                _connection.Close();
                return true;
            }
            catch (MySqlException)
            {
                return false;
            }
        }

        //  Function to check the table exists or not
        public static bool TableExists(string tableName)
        {
            try
            {
                var sql = string.Format("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '{0}' " +
                  "AND TABLE_NAME = '{1}'", _connection.Database, tableName);
                var command = new MySqlCommand(sql, _connection);
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
            catch (MySqlException)
            {
                return false;
            }
        }

    }
}
