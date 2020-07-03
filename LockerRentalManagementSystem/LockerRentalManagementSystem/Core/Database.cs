using System;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace LockerRentalManagementSystem.Core
{
    public static class Database
    {
        private static MySqlConnection _connection;
        public static MySqlConnection Connection
        {
            get { return _connection; }
        }

        private static string _databaseName;
        public static string DatabaseName
        {
            get { return _databaseName; }
        }

        public static void Initialize(string server, string port, string uid, string pw, string db)
        {
            _databaseName = db;
            string connString = String.Format("SERVER={0};PORT={1};UID={2};PASSWORD={3};DATABASE={4};SSLMODE=NONE", server, port, uid, pw, db);
            _connection = new MySqlConnection(connString);
        }

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

        public static bool Disconnect()
        {
            try
            {
                _connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Number.ToString() + " " + ex.Message);
                return false;
            }
        }

        public static void CreateDatabase(string server, string port, string uid, string pw, string db)
        {
            try
            {
                _databaseName = db;
                string connString = String.Format("SERVER={0};PORT={1};UID={2};PASSWORD={3};SSLMODE=NONE", server, port, uid, pw);
                MySqlConnection con = new MySqlConnection(connString);
                con.Open();
                var sql = string.Format("CREATE DATABASE IF NOT EXISTS {0}", db);
                var command = new MySqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Number.ToString() + " " + ex.Message);
            }

        }

        public static bool TableExists(string tableName)
        {
            try
            {
                var sql = string.Format("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '{0}' " +
                  "AND TABLE_NAME = '{1}'", _databaseName, tableName);
                var command = new MySqlCommand(sql, _connection);
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Number.ToString() + " " + ex.Message);
                return false;
            }
        }

        public static void DropTable()
        {
            var sql = "DROP TABLE IF EXISTS RENTAL; DROP TABLE IF EXISTS LOCKER; " +
                     "DROP TABLE IF EXISTS CABINET; DROP TABLE IF EXISTS LOCKER_TYPE; " +
                     "DROP TABLE IF EXISTS CUSTOMER; DROP TABLE IF EXISTS EMPLOYEE; ";
            MySqlCommand command = new MySqlCommand(sql, _connection);
            command.ExecuteNonQuery();
        }

        public static void CreateTable()
        {
            try
            {
                var str = "SET PERSIST information_schema_stats_expiry = 0";
                MySqlCommand cmd = new MySqlCommand(str, _connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

            }
            _connection.Close();
            _connection.Open();

            //serial is alias for BIGINT UNSIGNED NOT NULL AUTO_INCREMENT UNIQUE
            var sql = "CREATE TABLE EMPLOYEE(id int(10) UNSIGNED NOT NULL AUTO_INCREMENT," +
                "username varchar(25) NOT NULL UNIQUE, password varchar(64) NOT NULL," +
                "permission varchar(10) NOT NULL, name varchar(100) NOT NULL, " +
                "ic_passport varchar(20) NOT NULL UNIQUE, gender varchar(10) NOT NULL, " +
                "position varchar(10) NOT NULL, phone_no varchar(20), " +
                "email varchar(100) NOT NULL, home_address varchar(1000) NOT NULL, master_key varchar(64) NOT NULL UNIQUE, " +
                "status varchar(10) NOT NULL DEFAULT 'Default', PRIMARY KEY(id)) " +
                "ENGINE = InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = utf8;";
            MySqlCommand command = new MySqlCommand(sql, _connection);
            command.ExecuteNonQuery();

            sql = "CREATE TABLE CUSTOMER (id int(10) UNSIGNED NOT NULL AUTO_INCREMENT, " +
                "name varchar(100) NOT NULL, ic_passport varchar(20) NOT NULL UNIQUE, gender varchar(10) NOT NULL, " +
                "phone_no varchar(20), email varchar(100) NOT NULL, home_address varchar(1000) NOT NULL, " +
                "status varchar(10) NOT NULL DEFAULT 'Active', " +
                "PRIMARY KEY(id)) ENGINE = InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = utf8;";
            command = new MySqlCommand(sql, _connection);
            command.ExecuteNonQuery();

            sql = "CREATE TABLE LOCKER_TYPE (id int(10) UNSIGNED NOT NULL AUTO_INCREMENT, " +
                "name varchar(20) NOT NULL UNIQUE, code varchar(10) NOT NULL UNIQUE, " +
                "rate decimal(10,2) NOT NULL DEFAULT 0, status varchar(20) NOT NULL DEFAULT 'Active', " +
                "PRIMARY KEY(id)) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;";
            command = new MySqlCommand(sql, _connection);
            command.ExecuteNonQuery();

            sql = "CREATE TABLE CABINET (id int(10) UNSIGNED NOT NULL AUTO_INCREMENT, " +
                "code varchar(10) NOT NULL UNIQUE, cabinet_rows int(3) NOT NULL, " +
                "cabinet_columns int(3) NOT NULL, locker_type_id int(10) UNSIGNED NOT NULL, " +
                "status varchar(20) NOT NULL DEFAULT 'Available', " +
                "PRIMARY KEY(id), FOREIGN KEY(locker_type_id) REFERENCES LOCKER_TYPE(id)) " +
                "ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;";
            command = new MySqlCommand(sql, _connection);
            command.ExecuteNonQuery();

            sql = "CREATE TABLE LOCKER (id int(10) UNSIGNED NOT NULL AUTO_INCREMENT, " +
                "code varchar(20) NOT NULL UNIQUE, cabinet_id int(10) UNSIGNED NOT NULL, " +
                "door_status varchar(10) NOT NULL DEFAULT 'Locked', " +
                "status varchar(20) NOT NULL DEFAULT 'Available', PRIMARY KEY(id), " +
                "FOREIGN KEY(cabinet_id) REFERENCES CABINET(id)) " +
                "ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;";
            command = new MySqlCommand(sql, _connection);
            command.ExecuteNonQuery();

            sql = "CREATE TABLE RENTAL (id int(10) UNSIGNED NOT NULL AUTO_INCREMENT, " +
                "code varchar(20) NOT NULL UNIQUE, booking_date_time datetime NOT NULL, " +
                "start_date date NOT NULL, end_date date NOT NULL, duration int(10) NOT NULL, " +
                "rental_key varchar(64) NOT NULL UNIQUE, return_date_time datetime, " +
                "customer_id int(10) UNSIGNED NOT NULL, locker_id int(10) UNSIGNED NOT NULL, " +
                "employee_id int(10) UNSIGNED NOT NULL, status varchar(20) DEFAULT 'Not Started', " +
                "PRIMARY KEY(id), FOREIGN KEY(customer_id) REFERENCES CUSTOMER(id), " +
                "FOREIGN KEY(locker_id) REFERENCES LOCKER(id),  " +
                "FOREIGN KEY(employee_id) REFERENCES EMPLOYEE(id))" +
                "ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;";
            command = new MySqlCommand(sql, _connection);
            command.ExecuteNonQuery();
        }

        public static bool CheckUnique(string tableName, string conditionAttribute, string condition)
        {
            try
            {
                var sql = string.Format("SELECT COUNT(*) FROM (SELECT * FROM {0} WHERE {1} = '{2}') AS CheckUnique",
                  tableName, conditionAttribute, condition);
                var command = new MySqlCommand(sql, _connection);
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count < 1;
            }
            catch (MySqlException)
            {
                return true;
            }
        }
        public static bool CheckUnique(string tableName, string conditionAttribute, string condition, string id)
        {
            try
            {
                var sql = string.Format("SELECT COUNT(*) FROM (SELECT * FROM {0} WHERE {1} = '{2}' AND id <> {3}) AS CheckUnique;",
                  tableName, conditionAttribute, condition, id);
                var command = new MySqlCommand(sql, _connection);
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count < 1;
            }
            catch (MySqlException)
            {
                return true;
            }
        }
    }
}
