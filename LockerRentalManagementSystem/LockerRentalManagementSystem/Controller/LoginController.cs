using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LockerRentalManagementSystem.Core;
using LockerRentalManagementSystem.Exceptions;
using LockerRentalManagementSystem.Model;
using LockerRentalManagementSystem.View;
using MySql.Data.MySqlClient;

namespace LockerRentalManagementSystem.Controller
{
    public class LoginController
    {
        // Data Attributes
        private string _username;
        private string _userPassword;
        private string _server;
        private string _port;
        private string _uid;
        private string _dbName;
        private string _dbPassword;

        // Getter and Setters
        public string Username { get { return _username; } set { _username = value; } }
        public string UserPassword { get { return _userPassword; } set { _userPassword = value; } }
        public string Server { get { return _server; } set { _server = value; } }
        public string Port { get { return _port; } set { _port = value; } }
        public string Uid { get { return _uid; } set { _uid = value; } }
        public string DbName { get { return _dbName; } set { _dbName = value; } }
        public string DbPassword { get { return _dbPassword; } set { _dbPassword = value; } }

        // Constructor
        public LoginController() { }

        // Methods
        // Method to Login
        public Employee Login(string username, string password)
        {
            _username = username;
            _userPassword = Security.SHA256Hash(password);

            // Check if the account exists
            List<Employee> employees = Employee.Where(String.Format("username='{0}'", _username), 0, 1);
            Employee employee = new Employee();

            if (!employees.Any())
                throw new InvalidLoginException("Incorrect Username");
            else
            {
                employee = employees[0];
                if (!_userPassword.Equals(employee.Password))
                    throw new InvalidLoginException("Incorrect Password");
                if (employee.IsDisabled())
                    throw new InvalidLoginException("Disabled Account");
            }
            return employee;
        }

        // Method to Validate the Server Connection
        public bool ValidateConnection()
        {
            try
            {
                string connectionString = String.Format("SERVER={0};PORT={1};UID={2};PASSWORD={3};SSLMODE=NONE", _server, _port, _uid, _dbPassword);
                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();
                connection.Close();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        // Method to validate the Database
        public bool ValidateDatabase()
        {
            Database.Initialize(_server, _port, _uid, _dbPassword, _dbName);
            return Database.Connect();
        }

        // Method to validate the Database Tables
        public bool ValidateTable()
        {
            return Database.TableExists("employee") &&
                     Database.TableExists("customer") &&
                     Database.TableExists("locker_type") &&
                     Database.TableExists("cabinet") &&
                     Database.TableExists("locker") &&
                     Database.TableExists("rental");
        }

        // Method to validate if any Employee account exists in the database
        public bool ValidateAdminExist()
        {
            int noOfAdmin = Employee.Count("permission='Admin'");
            if (noOfAdmin > 0)
                return true;
            else
                return false;
        }

        // Method to create the new database
        public void CreateDatabase()
        {
            Database.CreateDatabase(_server, _port, _uid, _dbPassword, _dbName);
            Database.Connect();
        }


        // Method to create new Tables for a database
        public void CreateTable()
        {
            Database.CreateTable();
        }

        // Method to drop all Tables for a database
        public void DropTable()
        {
            Database.DropTable();
        }

        // Method to create the main admin account
        public void CreateAdmin()
        {
            EmployeeForm newAdminForm = new EmployeeForm(true);
            newAdminForm.ShowDialog();
        }

        // Method to check Db Configuration file exist or not
        public bool DbConfigExists()
        {
            if (!File.Exists("DbConfig.ini"))
                return false;
            else
                return true;
        }

        // Method to create new default Db Configuration file for reading
        public void WriteNewDbConfig()
        {
            _server = "";
            _port = "0";
            _uid = "";
            _dbPassword = "";
            _dbName = "";

            SaveIniFile();
        }

        //  Method to read the Database Configuration file
        public void LoadIniFile()
        {
            var LoadIni = new INIFile("DbConfig.ini");

            //  Load all the encypted attributes from the config file
            string encryptedServer = LoadIni.Read("server");
            string encryptedPort = LoadIni.Read("port");
            string encryptedUid = LoadIni.Read("uid");
            string encryptedPw = LoadIni.Read("password");
            string encryptedDb = LoadIni.Read("database");

            //  Decrypt all the attributes readed form the config file
            _server = Security.EncryptDecrypt(encryptedServer, 94);
            _port = Security.EncryptDecrypt(encryptedPort, 83);
            _uid = Security.EncryptDecrypt(encryptedUid, 56);
            _dbPassword = Security.EncryptDecrypt(encryptedPw, 38);
            _dbName = Security.EncryptDecrypt(encryptedDb, 21);

        }

        //  Method to Save the Database Configuration file
        public void SaveIniFile()
        {
            //  Encrypt all the attributes
            string encryptedServer = Security.EncryptDecrypt(_server, 94);
            string encryptedPort = Security.EncryptDecrypt(_port, 83);
            string encryptedUid = Security.EncryptDecrypt(_uid, 56);
            string encryptedPw = Security.EncryptDecrypt(_dbPassword, 38);
            string encryptedDb = Security.EncryptDecrypt(_dbName, 21);

            //  Save the encrypted attributes into the config file
            var newIni = new INIFile("DbConfig.ini");
            newIni.Write("server", encryptedServer);
            newIni.Write("port", encryptedPort);
            newIni.Write("uid", encryptedUid);
            newIni.Write("password", encryptedPw);
            newIni.Write("database", encryptedDb);
        }


    }
}
